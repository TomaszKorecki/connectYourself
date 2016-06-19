'use strict';
app.controller('deviceController', ['$scope', '$routeParams', 'authService', 'devicesService', 'toaster', 'signalRService',
	function ($scope, $routeParams, authService, deviceService, toaster, signalRService) {
		$scope.authentication = authService.authentication;

		$scope.messageToDevice = null;

		$scope.pop = function () {
			toaster.pop('success', "title", "text");
		};

		$scope.setDeviceState = {
			deviceState: ""
		};

		$scope.deviceStatesHistoryDownloaded = false;

		var deviceId = $routeParams.device_id;
		var connection = $.hubConnection("http://localhost:55932/signalR");
		var hub = connection.createHubProxy("UsersHub");

		$scope.onSetDeviceState = function () {
			toaster.pop({ type: 'wait', body: "Setting up new device state", toastId: 1 });

			var message = {
				deviceState: $scope.setDeviceState.deviceState,
				secretKey: $scope.device.secretKey,
				deviceName: $scope.device.deviceName
			}

			deviceService.setDeviceState(message, function (result) {
				toaster.clear(null, 1);
			}, function (error) {
				toaster.clear(null, 1);
			});
		}


		//---------------- Controller logic ---------------------

		hub.on("NotifyAboutDeviceStateChange", function (response) {
			toaster.clear();
			console.log("Response from device state notification: ");
			console.log(response);
			toaster.pop({ type: 'success', body: "Device state changed to: " + response.State, timeOut: 1500, toastId: 2 });
			if ($scope.device.id.localeCompare(response.Id) === 0) {
				var newStateHistoryElement = {
					state: response.State,
					stateTransitionDateTime: response.DateTime
				}

				$scope.deviceStatesHistory.unshift(newStateHistoryElement);
				$scope.device.actualState = response.State;
			}
		});

		hub.on("NotifyAboutDeviceMessageReceived", function (response) {
			toaster.clear();
			console.log("Response from device message notification: ");
			console.log(response);
			toaster.pop({ type: 'success', body: "Device message received: " + response.Message, timeOut: 1500, toastId: 2 });
			if ($scope.device.id.localeCompare(response.Id) === 0) {
				var newMessageElement = {
					messageContent: response.Message,
					messageDateTime: response.DateTime
				}

				$scope.deviceMessagesHistory.unshift(newMessageElement);
			}
		});

		

		connection.start().done(function() {
			hub.invoke("RegisterUser", authService.authentication.userName);
		});

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
			toaster.pop({ type: 'error', body: "Unable to find the device states history" });
		});

		deviceService.getDeviceMessagesHistory(deviceId, function (result) {
			$scope.deviceMessagesHistory = result.data;
			$scope.deviceMessagesHistoryDownloaded = true;
			console.log($scope.deviceMessagesHistory);
		}, function (error) {
			toaster.pop({ type: 'error', body: "Unable to find the device messages history" });
		});
	}]);