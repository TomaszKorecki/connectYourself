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

	var onRemoveDevice = function (device) {
		return $http.delete(serviceBaseUri + 'api/devices/' + device.id).then(function (result) {
			return result;
		});
	}

	var onChangeDevice = function (device) {
		return $http.put(serviceBaseUri + 'api/devices/', device).then(function (result) {
			return result;
		});
	}



	devicesServiceFactory.getDevices = getDevices;
	devicesServiceFactory.addNewDevice = addNewDevice;
	devicesServiceFactory.onRemoveDevice = onRemoveDevice;
	devicesServiceFactory.onChangeDevice = onChangeDevice;

	return devicesServiceFactory;

}]);