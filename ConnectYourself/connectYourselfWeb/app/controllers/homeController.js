﻿'use strict';
app.controller('homeController', ['$scope', '$location', 'authService', 'devicesService', 'toaster', 'signalRService',
	function ($scope, $location, authService, deviceService, toaster, signalRService) {
		$scope.authentication = authService.authentication;

		$scope.pop = function () {
			toaster.pop('success', "title", "text");
		};

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

		$scope.onAddDevice = function () {
			console.log($scope.newDeviceData);
			toaster.pop({ type: 'wait', body: "Adding device", toastId: 1 });
			deviceService.addNewDevice($scope.newDeviceData).then(function (result) {
				$scope.userDevices.push(result.data);
				toaster.pop({ type: 'success', body: "Added successfully" });
				toaster.clear(null, 1);
			});
		}

		$scope.onDeviceRemove = function (idx) {
			var deviceToRemove = $scope.userDevices[idx];
			console.log(deviceToRemove);
			toaster.pop({ type: 'wait', body: "Removing device", toastId: 3 });
			deviceService.onRemoveDevice(deviceToRemove, function (result) {
				$scope.userDevices.splice(idx, 1);
				toaster.clear(null, 3);
				toaster.pop({ type: 'success', body: "Removed successfully" });
			}, function (error) {
				toaster.clear(null, 3);
				toaster.pop({ type: 'warning', body: "Removed fail" });
			});
		}

		$scope.onDeviceChangeCacheDataMode = function (idx) {
			var deviceToChange = $scope.userDevices[idx];
			deviceToChange.cacheData = !deviceToChange.cacheData;
			deviceService.onChangeDevice(deviceToChange).then(function (result) {
				toaster.pop({ type: 'success', body: "Changed settings successfully" });
			});
		}

		$scope.onDeviceReconnect = function (idx) {
			var deviceToReconnect = $scope.userDevices[idx];
			toaster.pop({ type: 'wait', body: "Reconnecting device", toastId: 2, timeout: 10000 });

			deviceService.onReconnectDevice(deviceToReconnect).then(function (result) {

				toaster.clear(null, 2);
			});
		}

		$scope.onDeviceInfo = function (idx) {
			var device = $scope.userDevices[idx];
			$location.path('/device/' + device.id);
		}

		//---------------- Controller logic ---------------------

		deviceService.getDevices().then(function (result) {
			$scope.userDevices = result.data;
			console.log($scope.userDevices);
		}, function (error) {
			alert(error.data.message);
		});
	}]);