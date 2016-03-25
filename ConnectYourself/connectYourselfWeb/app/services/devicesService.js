'use strict';
app.factory('devicesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

	var serviceBaseUri = ngAuthSettings.apiServiceBaseUri;

	var devicesServiceFactory = {};

	var getDevices = function () {
		return $http.get(serviceBaseUri + 'api/devices').then(function (results) {
			return results;
		});
	};

	var addNewDevice = function(newDeviceData) {
		return $http.post(serviceBaseUri + 'api/devices', newDeviceData).then(function (results) {
			return results;
		});
	}

	devicesServiceFactory.getDevices = getDevices;
	devicesServiceFactory.addNewDevice = addNewDevice;

	return devicesServiceFactory;

}]);