'use strict';
app.controller('homeController', ['$scope', 'authService', 'devicesService', function ($scope, authService, deviceService) {
	$scope.authentication = authService.authentication;

	$scope.newDeviceData = {
		name: "",
		cacheData: ""
	};

	$scope.hoverIn = function () {
		this.hoverEdit = true;
	};

	$scope.hoverOut = function () {
		this.hoverEdit = false;
	};

	$scope.onAddDevice = function() {
		console.log($scope.newDeviceData);
		deviceService.addNewDevice($scope.newDeviceData).then(function(result) {
			$scope.userDevices = result.data;
		});
	}

	$scope.onRemoveDevice = function (idx) {
		var deviceToRemove = $scope.userDevices[idx];
		console.log(deviceToRemove);
		deviceService.onRemoveDevice(deviceToRemove);
		$scope.userDevices.splice(idx, 1);
	}

	deviceService.getDevices().then(function (result) {
		$scope.userDevices = result.data;
		console.log($scope.userDevices);
	}, function (error) {
		alert(error.data.message);
	});
}]);