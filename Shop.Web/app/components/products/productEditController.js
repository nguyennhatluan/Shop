(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject['$stateParams', '$scope', '$http', 'notificationService', '$state','commonService'];
    function productEditController($stateParams, $scope, $http, notificationService, $state, commonService) {
        $scope.loadProductDetail = loadProductDetail;
        $scope.loadProductCategory = loadProductCategory;
        $scope.editProduct = editProduct;
        $scope.getSeoTitle = getSeoTitle;

        $scope.ckeditorOptions = {
            language: 'vi',
            height:'200px'
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }

        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function editProduct() {
            $http({
                url: '/api/product/update',
                method: 'put',
                data: $scope.product
            }).then(function (result) {
                notificationService.displaySuccess('Đã cập nhập sản phẩm ' + result.data.Name);
                $state.go('products');
            }, function () {
                    notificationService.displayError('Không thể cập nhập sản phẩm')
            });
               
        }

        function loadProductCategory() {
            $http({
                url: '/api/productcategory/getallparents',
                method: 'GET',

            }).then(function (result) {
                $scope.Categories = result.data;
            }, function () {
                console.log('Không thể load productcategories');
            });
        }

        function loadProductDetail() {
            $http({
                url: '/api/product/getbyid',
                params: { id: $stateParams.id},
                method: 'GET'
            }).then(function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse(result.data.MoreImage);
            }, function () {
                    console.log('Không thể load');
            });
        }
        $scope.loadProductCategory();
        $scope.loadProductDetail();
    }
})(angular.module('shop.products'));