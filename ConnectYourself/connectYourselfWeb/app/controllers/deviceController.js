'use strict';
app.controller('deviceController', ['$scope', '$routeParams', 'authService', 'devicesService', 'toaster', function ($scope, $routeParams, authService, deviceService, toaster) {
	$scope.authentication = authService.authentication;

	$scope.pop = function () {
		toaster.pop('success', "title", "text");
	};

	var deviceId = $routeParams.device_id;

	//---------------- Controller logic ---------------------

	deviceService.getDevice(deviceId, function (result) {
		$scope.device = result.data;
		console.log($scope.device);
	}, function (error) {
		alert(error.data.message);
	});
}]);