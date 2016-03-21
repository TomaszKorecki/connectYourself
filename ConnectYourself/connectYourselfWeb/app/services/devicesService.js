'use strict';
app.factory('devicesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

	var serviceBaseUri = ngAuthSettings.apiServiceBaseUri;

	var devicesServiceFactory = {};

	var getDevices = function () {

		return $http.get(serviceBaseUri + 'api/orders').then(function (results) {
			return results;
		});
	};

	var addNewDevice = function() {
		
	}

	devicesServiceFactory.getDevices = getDevices;
	devicesServiceFactory.addNewDevice = addNewDevice;

	return devicesServiceFactory;

}]);