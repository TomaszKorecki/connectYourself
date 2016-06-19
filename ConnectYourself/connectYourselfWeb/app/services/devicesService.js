'use strict';
app.factory('devicesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

	var serviceBaseUri = ngAuthSettings.apiServiceBaseUri;

	var devicesServiceFactory = {};

	var getDevice =  function(deviceId, successCallback, errorCallback) {
		return $http.get(serviceBaseUri + 'api/devices/' + deviceId).then(function(result) {
			successCallback(result);
		}, function(error) {
			errorCallback(error);
		});
	}

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

	var onRemoveDevice = function (device, successCallback, errorCallback) {
		$http.delete(serviceBaseUri + 'api/devices/' + device.id).then(function (result) {
			successCallback(result);
		}, function(error) {
			errorCallback(error);
		});
	}

	var onChangeDevice = function (device) {
		return $http.put(serviceBaseUri + 'api/devices/', device).then(function (result) {
			return result;
		});
	}

	var onReconnectDevice = function(device) {
		return $http.post(serviceBaseUri + 'api/devices/reconnect', { deviceId: device.id }).then(function (results) {
			return results;
		});
	}

	var getDeviceStatesHistory = function(deviceId, successCallback, errorCallback) {
		return $http.get(serviceBaseUri + 'api/devices/getDeviceStatesHistory/' + deviceId).then(function (results) {
			successCallback(results);
		}, function(error) {
			errorCallback(error);
		});
	}

	var getDeviceMessagesHistory = function (deviceId, successCallback, errorCallback) {
		return $http.get(serviceBaseUri + 'api/devices/getDeviceMessagesHistory/' + deviceId).then(function (results) {
			successCallback(results);
		}, function (error) {
			errorCallback(error);
		});
	}

	var setDeviceState = function (deviceState, successCallback, errorCallback) {
		return $http.post(serviceBaseUri + 'api/devices/setDeviceState', deviceState).then(function (results) {
			successCallback(results);
		}, function (error) {
			errorCallback(error);
		});
	}

	devicesServiceFactory.getDevice = getDevice;
	devicesServiceFactory.getDevices = getDevices;
	devicesServiceFactory.addNewDevice = addNewDevice;
	devicesServiceFactory.onRemoveDevice = onRemoveDevice;
	devicesServiceFactory.onChangeDevice = onChangeDevice;
	devicesServiceFactory.onReconnectDevice = onReconnectDevice;
	devicesServiceFactory.getDeviceStatesHistory = getDeviceStatesHistory;
	devicesServiceFactory.getDeviceMessagesHistory = getDeviceMessagesHistory;
	devicesServiceFactory.setDeviceState = setDeviceState;

	return devicesServiceFactory;
}]);