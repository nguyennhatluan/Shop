/// <reference path="../plugins/angular.min.js" />
var myApp = angular.module("myModule", []);
myApp.controller("myController", myController);
myController.$inject = ['$scope'];
function myController($scope) {
    $scope.message = "This is my message from controller";
    $scope.name = "Nguyễn Nhất Luân";
}