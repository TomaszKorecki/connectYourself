'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', 'toaster', 'signalRService', function ($scope, $location, authService, toaster, signalRService) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.authentication = authService.authentication;
}]);