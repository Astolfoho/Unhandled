
var app = angular.module("app", ["ngRoute", "ngResource"]);


module Unhandled {

    class Config {
        constructor($routeProvider: ng.route.IRouteProvider, $locationProvider: ng.ILocationProvider) {
            $routeProvider
            .when("/Home", {
                templateUrl: "/SpaTemplates/Home/Index.html"
            })
            .when("/Home/Index", {
                templateUrl: "/SpaTemplates/Home/Index.html"
            })
            .otherwise({
                redirectTo: '/Home'
                });

            $locationProvider.html5Mode(true);

        }

    }

    Config.$inject = ['$routeProvider', '$locationProvider'];
    app.config(Config);

}

