'use strict';
app.controller('notificationsController', ['$scope', '$location', 'authService', 'notificationService', 'toaster',
	function ($scope, $location, authService, notificationService, toaster) {
		$scope.authentication = authService.authentication;

		$scope.pop = function () {
			toaster.pop('success', "title", "text");
		};

		$scope.addNewNotificationRule = {
			sourceDeviceName: "",
			sourceMessage: "",
			targetDeviceName: "",
			targetMessage: ""
		}

		$scope.onAddNewNotificationRule = function() {
			notificationService.onAddNewNotificationRule($scope.addNewNotificationRule, function(results) {
				
			}, function(error) {
				toaster.pop('error', "Wrong devices names", "");
			});
		}

		//---------------- Controller logic ---------------------
		notificationService.getNotificationRules(function (results) {
			$scope.notificationRules = results.data;
		});
	}]);