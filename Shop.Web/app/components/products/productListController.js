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
    productListController.$inject['$scope', '$http', 'notificationService','$ngBootbox','$state','$filter'];

    function productListController($scope, $http, notificationService, $ngBootbox, $state, $filter) {
        $scope.page = 0;
        $scope.pageSize = 20;
        $scope.keyWord = '';
        $scope.products = [];

        $scope.getListProducts = getListProducts;
        $scope.deleteProduct = deleteProduct;
        $scope.selectAll = selectAll;
        $scope.deleteMulti = deleteMulti;

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch('products', function(newValue, oldValue) {
            console.log(newValue);
            var checked = $filter('filter')(newValue, { checked: true });
            if (checked.length) {
                $('#btnDelete').removeAttr('disabled');
                $scope.selected = checked;
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteMulti() {
            $ngBootbox.confirm('Bạn có muốn xóa không').then(function () {
                var listId = [];
                angular.forEach($scope.selected, function (item) {
                    if (item.checked === true) {
                        listId.push(item.ID);
                    }
                });

                $http({
                    url: '/api/product/deletemulti',
                    method: 'DELETE',
                    params: { stringID: JSON.stringify(listId) }
                }).then(function (result) {
                    notificationService.displaySuccess('Đã xóa ' + result.data + ' sản phẩm');
                    
                    $scope.getListProducts();
                }, function () {
                    notificationService.displayError('Không thể xóa sản phẩm');
                });
            });
        }

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
                    $scope.products = result.data.Items;
                    $scope.totalCount = result.data.TotalCount;
                }
            }, function (error) {
                    notificationService.displayError('Error');
            });
        }

        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn có muốn xóa không ').then(function () {
                $http({
                    url: '/api/product/delete',
                    method: 'DELETE',
                    params: {id:id}
                }).then(function () {
                    notificationService.displaySuccess('Đã xóa sản phẩm');
                    $scope.getListProducts();

                }, function () {
                    notificationService.displayError('Không thể xóa sản phẩm');
                });
            });
        }

        $scope.getListProducts();
    }

})(angular.module('shop.products'));