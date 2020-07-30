﻿/// <reference path="../../../scripts/plugins/angular.min.js" />

(function () {
    angular.module('shop.products', ['shop.common']).config(config);

    config.$inject['$stateProvider'];

    function config($stateProvider) {
        $stateProvider.state('products', {
            url: '/products',
            parent:'base',
            templateUrl: '/app/components/products/productListView.html',
            controller: 'productListController'
        }).state('products_add', {
            url: '/products_add',
            parent: 'base',
            templateUrl: '/app/components/products/productAddView.html',
            controller: 'productAddController'
        }).state('products_edit', {
            url: '/products_edit/:id',
            parent: 'base',
            templateUrl: '/app/components/products/productEditView.html',
            controller: 'productEditController'
        });
    }
})();