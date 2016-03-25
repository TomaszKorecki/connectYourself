'use strict';
app.controller('homeController', ['$scope', 'authService', 'devicesService', function ($scope, authService, deviceService) {
	$scope.authentication = authService.authentication;

	$scope.newDeviceData = {
		name: "",
		cacheData: ""
	};

	$scope.onAddDevice = function() {
		console.log($scope.newDeviceData);
		deviceService.addNewDevice($scope.newDeviceData).then(function(result) {
			$scope.userDevices = result.data;
		});
	}

	deviceService.getDevices().then(function (result) {
		$scope.userDevices = result.data;
		console.log($scope.userDevices);
	}, function (error) {
		alert(error.data.message);
	});
}]);