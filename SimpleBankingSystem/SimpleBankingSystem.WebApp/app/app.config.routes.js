angular.module('app')
.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true);

    $routeProvider.when('/home', {
        templateUrl: '/app/pages/home/home.html',
        controller: 'HomeCtrl',
        caseInsensitiveMatch: true
    }).when('/account/:accountId', {
        templateUrl: '/app/pages/account/account.html',
        controller: 'AccountCtrl',
        caseInsensitiveMatch: true
    }).otherwise({
        redirectTo: '/home'
    });
}]);