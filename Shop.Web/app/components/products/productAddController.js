//(function (app) {
//    app.controller('productAddController', productAddController);
//    productAddController.$inject['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
//    function productAddController($scope, apiService, notificationService, $state, commonService) {
//        $scope.product = {

//        }
//        $scope.AddProduct = AddProduct;
        
//        function LoadCategory() {
//            apiService.get('/api/product/getallcategory', null, function (result) {
//                $scope.categories = result.data;
//            }, function (error) {
//                console.log('Cannot get categories');
//            });
//        }
//        function AddProduct() {
//            apiService.post('/api/product/create', $scope.product, function (result) {
//                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
//                $state.go('products');
//            }, function (error) {
//                    notificationService.displayError('Thêm mới sản phẩm không thành công');
//            });
//        }

//        LoadCategory();
//    }
//})(angular.module('shop.products'));

(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject['$scope','$http','notificationService','commonService','$state'];
    function productAddController($scope, $http, notificationService,commonService,$state) {

        $scope.getAllCategories = getAllCategories;
        $scope.addProduct = addProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.product = {
            Status: true,
            Image: '/UploadedFiles/images/image-post-none.png',
            
        }
        $scope.moreImages = [];

        $scope.ChooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
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

        function addProduct() {
            $scope.product.MoreImage = JSON.stringify($scope.moreImages);
            $http({
                url: '/api/product/create',
                method: 'POST',
                data: $scope.product
            }).then(function (result) {
                notificationService.displaySuccess('Đã thêm sản phẩm ' + result.data.Name);
                $state.go('products');
            }, function (error) {
                    notificationService.displayError('Không thể thêm sản phẩm');
            });
        }


        function getAllCategories() {
            $http({
                url: '/api/product/getallcategory',
                method:'GET'
            })
                .then(function (result) {
                    $scope.categories = result.data;
            },
            function (error) {
               console.log('Cannot load productcategory');
            })
        }

        $scope.ckeditorOption = {
            languague: 'vi',
            height:'200px'
        }

        $scope.getAllCategories();

    }
})(angular.module('shop.products'));