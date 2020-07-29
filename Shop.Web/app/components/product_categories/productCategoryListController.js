(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.keyWord = '';
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;

        $scope.searchKeyWord = function () {
            getProductCagories();
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function deleteMultiple() {
            var listId = [];
            angular.forEach($scope.selected, function (item) {
                listId.push(item.ID);
            });
            $ngBootbox.confirm("Bạn có chắc muốn xóa không ?").then(function () {
                var config = {
                    params: {
                        checkedProductCategories: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/productCategory/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Đã xóa ' + result.data + ' bản ghi');
                    console.log($scope.selected);
                    $scope.getProductCategories();
                },
                    function (error) {
                        notificationService.displayError('Không thể xóa');
                    })
            });
        }

        $scope.$watch("productCategories", function (n, o) {
            console.log(n);
            var checked = $filter("filter")(n, { checked: true }); // lấy ra danh sách các checked
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProductCategory(id) {
            $ngBootbox.confirm("Bạn có chắc muốn xóa không ?").then(function () {
                var config = {
                    params: {
                        id:id
                    }
                }
                apiService.del('/api/productCategory/delete', config, function (result) {
                    notificationService.displaySuccess('Đã xóa');
                    $scope.getProductCategories();
                },
                    function (error) {
                        notificationService.displayError('Không thể xóa');
                    })
            });
        }

        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 2
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                else {
                    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.');
                }
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }

        $scope.getProductCategories();
    }
})(angular.module('shop.product_categories'));