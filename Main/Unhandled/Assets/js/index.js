/// <reference path="angular.min.js" />
/// <reference path="angular-route.min.js" />


angular.module("unhandled", ["ngRoute", "ngSanitize"])



.config(function($routeProvider, $locationProvider) {
    $routeProvider
     //.when('/Book/:bookId', {
     //    templateUrl: 'book.html',
     //    controller: 'BookController',
     //    resolve: {
     //        // I will cause a 1 second delay
     //        delay: function($q, $timeout) {
     //            var delay = $q.defer();
     //            $timeout(delay.resolve, 1000);
     //            return delay.promise;
     //        }
     //    }
     //})

    .when('/ErrosList', {
        templateUrl: 'error_list.html.unhandled.axd'
    })

    .when('/ErrosDetails/:id', {
        templateUrl: 'error_details.html.unhandled.axd'
    })

    .when('/CookieList/:idError', {
        templateUrl: 'cookie_list.html.unhandled.axd'
    })

    .otherwise({
        redirectTo: '/ErrosList',
    });

    // configure html5 to get links working on jsfiddle
    $locationProvider.html5Mode(false);
})


.controller("ErrorListCtrl", ["$http", ErrorListCtrl])
.controller("ErrorDetailsCtrl", ["$http", "$routeParams", ErrorDetailsCtrl])
.controller("CookieListCtrl", ["$http", "$routeParams", CookieListCtrl]);

function ErrorListCtrl($http) {
    var that = this;
    that.items = [];

    $http.get("unhandled.axd?method=GetErrors")
    .success(function (data) {
        that.items = data;
    });
    return that;
}

function ErrorDetailsCtrl($http, $routeParams) {
    var that = this;
    that.model = [];

    $http.get("unhandled.axd?method=GetErrorDetails&id=" + $routeParams.id)
    .success(function (data) {
        that.model = data;
    });
    return that;
}

function CookieListCtrl($http, $routeParams) {
    var that = this;
    that.model = [];

    

    $http.get("unhandled.axd?method=GetCookieList&idError=" + $routeParams.idError)
    .success(function (data) {
        that.model = data.d;
        for (var i = 0; i < data.d.length ; i++) {            
            that.model[i].expires = parseJsonDate(that.model[i].expires);
        } 

    });
    return that;
}


function parseJsonDate(jsonDate) {

    var datereg = /\/Date\(((-?|\+?)[0-9]*)-.*\)/;
    var mils = datereg.exec(jsonDate)[1];

    var date = new Date(+mils);
    var dd = date.getDate();
    var mm = date.getMonth() + 1; //January is 0!

    var yyyy = date.getFullYear().toString();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    
    var formatedYear = ('0000' + yyyy).substring(yyyy.length);

    var date = dd + '/' + mm + '/' + formatedYear;
    return date;
}
