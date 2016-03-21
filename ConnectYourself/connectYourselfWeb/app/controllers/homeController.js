'use strict';
app.controller('homeController', ['$scope', 'authService', function ($scope, authService) {
	$scope.authentication = authService.authentication;

	$scope.newDeviceData = {
		name: "",
		cacheData: ""
	};

	$scope.onAddDevice = function() {
		console.log($scope.newDeviceData);
	}
}]);