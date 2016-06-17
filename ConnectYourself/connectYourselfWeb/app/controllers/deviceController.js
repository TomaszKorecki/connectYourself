'use strict';
app.controller('deviceController', ['$scope', '$routeParams', 'authService', 'devicesService', 'toaster', function ($scope, $routeParams, authService, deviceService, toaster) {
	$scope.authentication = authService.authentication;

	$scope.pop = function () {
		toaster.pop('success', "title", "text");
	};

	var deviceId = $routeParams.device_id;
	$scope.deviceStatesHistoryDownloaded = false;

	//---------------- Controller logic ---------------------

	deviceService.getDevice(deviceId, function (result) {
		$scope.device = result.data;
		console.log($scope.device);
	}, function (error) {
		alert(error.data.message);
	});


	deviceService.getDeviceStatesHistory(deviceId, function (result) {
		$scope.deviceStatesHistory = result.data;
		$scope.deviceStatesHistoryDownloaded = true;
		console.log($scope.deviceStatesHistory);
	}, function (error) {
		toaster.pop({ type: 'error', body: "Unable to find the device history" });
	});

	deviceService.getDeviceMessagesHistory(deviceId, function (result) {
		$scope.deviceMessagesHistory = result.data;
		$scope.deviceMessagesHistory = true;
		console.log($scope.deviceMessagesHistory);
	}, function (error) {
		toaster.pop({ type: 'error', body: "Unable to find the device history" });
	});
	
}]);