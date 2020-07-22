(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.product = {

        }
        $scope.AddProduct = AddProduct;
        
        function LoadCategory() {
            apiService.get('/api/product/getallcategory', null, function (result) {
                $scope.categories = result.data;
            }, function (error) {
                console.log('Cannot get categories');
            });
        }
        function AddProduct() {
            apiService.post('/api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('products');
            }, function (error) {
                    notificationService.displayError('Thêm mới sản phẩm không thành công');
            });
        }

        LoadCategory();
    }
})(angular.module('shop.products'));