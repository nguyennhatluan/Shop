//(function (app) {
//    app.controller('productListController', productListController);
//    productListController.$inject['apiService', '$scope', 'notificationService'];
//    function productListController(apiService, $scope, notificationService) {

//        $scope.products = [];
//        $scope.page = 0;
//        $scope.pagesCount = 0;
//        $scope.getProducts = getProducts;
//        $scope.keyWord = '';

//        function getProducts(page) {
//            page = page || 0;
//            var config = {
//                params: {
//                    keyWord: $scope.keyWord,
//                    page: page,
//                    pageSize: 20
//                }
//            };
//            apiService.get('/api/product/getall', config, function (result) {
//                if (result.data.TotalCount == 0) {
//                    notificationService.displayWarning("Không tìm thấy bản ghi nào");
//                }
//                else {
//                    notificationService.displaySuccess("Đã tìm thấy " + result.data.TotalCount + " bản ghi");
//                    $scope.products = result.data.Items;
//                    $scope.page = result.data.page;
//                    $scope.pagesCount = result.data.TotalPages;
//                    $scope.totalCount = result.data.TotalCount;
//                }
//            }, function (error) {
//                    console.log('Load productcategory failed.');
//            });
//        };
//        $scope.getProducts();
//    }
//})(angular.module('shop.products'));

//(function (app) {
//    app.controller('productListController', productListController);
//    productListController.$inject['$scope', 'apiService', 'notificationService'];
//    function productListController($scope,apiService,notificationService) {
//        $scope.page = 0;
//        $scope.pageSize = 20;
//        $scope.keyWord = '';
//        $scope.products = [];

//        $scope.getAllProducts = getAllProducts;

//        function getAllProducts(page) {
//            page = page || 0;
//            var config = {
//                params: {
//                    page: page,
//                    keyWord: $scope.keyWord,
//                    pageSize: $scope.pageSize
//                }

//            }
//            apiService.get('/api/product/getall', config, function (result) {
//                if (result.data.TotalCount > 0) {
//                    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi');
//                    $scope.products = result.data.Items;
//                    $scope.totalCount = result.data.TotalCount;
//                    $scope.pageCount = result.data.TotalPages;
//                    $scope.page = result.data.Page;
//                }
//                else {
//                    notificationService.displaySuccess('Không tìm thấy bản ghi nào');
//                }
//            }, function (error) {
//                    console.log('Lỗi không thể load được dữ liệu');
//            });
//        }
//        $scope.getAllProducts();

//    }
//})(angular.module('shop.products'));

(function (app) {

    app.controller('productListController', productListController);
    productListController.$inject['$scope','$http','notificationService'];

    function productListController($scope, $http, notificationService) {
        $scope.page = 0;
        $scope.pageSize = 20;
        $scope.keyWord = '';
        $scope.products = [];

        $scope.getListProducts = getListProducts;

        

        function getListProducts(page) {
            page = page || 0;
            $http({
                url: '/api/product/getall',
                method: 'GET',
                params: { page: page, keyWord: $scope.keyWord, pageSize: $scope.pageSize }
            }).then(function (result) {
                if (result.data.TotalCount > 0) {
                    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi');
                    $scope.products = result.data.Items;
                    $scope.page = page;
                    $scope.totalPage = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                }
                else {
                    notificationService.displayError('Không tìm thấy');
                }
            }, function (error) {
                    notificationService.displayError('Error');
            });
        }

        $scope.getListProducts();
    }

})(angular.module('shop.products'));