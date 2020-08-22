/// <reference path="../../../scripts/plugins/angular.js" />
(function () {
    angular.module('shop.product_categories', ['shop.common']).config(config);
    
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('product_categories', {
            url: "/product_categories",
            templateUrl: "/app/components/product_categories/productCategoryListView.html",
            parent: 'base',
            controller: "productCategoryListController"
        }).state('add_product_categories', {
            url: "/add_product_categories",
            templateUrl: "/app/components/product_categories/productCategoryAddView.html",
            parent: 'base',
            controller: "productCategoryAddController"
        }).state('edit_product_categories', {
            url: "/edit_product_category/:id",
            templateUrl: "/app/components/product_categories/productCategoryEditView.html",
            parent: 'base',
            controller: "productCategoryEditController"
        });
    }
})();