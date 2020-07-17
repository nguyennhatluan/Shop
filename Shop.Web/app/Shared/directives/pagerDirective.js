(function (app) {
    'use strict';

    app.directive('pagerDirective', pagerDirective);

    function pagerDirective() {
        return {
            scope: {
                page: '@', // @ là nhận dữ liệu 1 chiều từ scope của controller đến scope của directive
                pagesCount: '@',
                totalCount: '@',
                searchFunc: '&',
                customPath: '@' // & nhận các method 1 chiều từ scope của controller đến scope của directive
            },
            replace: true, //replace: true có nghĩa là nội dung của mẫu lệnh sẽ thay thế phần tử mà lệnh được khai báo
            restrict: 'E',
            templateUrl: '/app/shared/directives/pagerDirective.html',
            controller: [
                '$scope', function ($scope) {
                    $scope.search = function (i) {
                        if ($scope.searchFunc) {
                            $scope.searchFunc({ page: i });
                        }
                    };

                    $scope.range = function () {
                        if (!$scope.pagesCount) { return []; }
                        var step = 2;
                        var doubleStep = step * 2;
                        var start = Math.max(0, $scope.page - step);
                        var end = start + 1 + doubleStep;
                        if (end > $scope.pagesCount) { end = $scope.pagesCount; }

                        var ret = [];
                        for (var i = start; i != end; ++i) {
                            ret.push(i);
                        }

                        return ret;
                    };

                    $scope.pagePlus = function (count) {
                        return +$scope.page + count;
                    }

                }]
        }
    }

})(angular.module('shop.common'));