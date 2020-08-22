/// <reference path="../../../scripts/plugins/angular.js" />


(function () {
    angular.module('shop.contact', ['shop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('contact_details', {
            url: "/contact_details",
            templateUrl: "/app/components/contact/ContactDetailView.html",
            parent: 'base',
            controller: "contactDetailController"
        }).state('contact_add', {
            url: "/contact_add",
            templateUrl: "/app/components/contact/ContactAddView.html",
            parent: 'base',
            controller: "contactAddController"
        }).state('contact_edit', {
            url: "/contact_edit/:id",
            templateUrl: "/app/components/contact/ContactEditView.html",
            parent: 'base',
            controller: "contactEditController"
        });
    }
})();