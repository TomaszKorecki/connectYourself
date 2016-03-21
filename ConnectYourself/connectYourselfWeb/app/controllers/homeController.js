'use strict';
app.controller('homeController', ['$scope', 'authService', 'devicesService', function ($scope, authService, deviceService) {
	$scope.authentication = authService.authentication;

	$scope.newDeviceData = {
		name: "",
		cacheData: ""
	};

	$scope.onAddDevice = function() {
		//console.log($scope.newDeviceData);
	}

	//deviceService.getDevices().then(function (results) {
	//	$scope.devices = results.data;

	//}, function (error) {
	//	//alert(error.data.message);
	//});
}]);