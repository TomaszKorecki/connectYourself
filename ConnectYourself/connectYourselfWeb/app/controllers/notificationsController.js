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

		$scope.onAddNewNotificationRule = function () {
			notificationService.onAddNewNotificationRule($scope.addNewNotificationRule, function(results) {
				$scope.notificationRules.unshift(results.data);
			}, function(error) {
				toaster.pop('error', "Wrong devices names", "");
			});
		}

		$scope.onRemoveNotificationRule = function(idx) {
			var ruleToRemove = $scope.notificationRules[idx];
			toaster.pop({ type: 'wait', body: "Removing notificatin rule...", toastId: 3 });
			notificationService.onRemoveNotificationRule(ruleToRemove, function (result) {
				$scope.notificationRules.splice(idx, 1);
				toaster.clear(null, 3);
				toaster.pop({ type: 'success', body: "Removed successfully" });
			}, function (error) {
				toaster.clear(null, 3);
				toaster.pop({ type: 'warning', body: "Removed fail" });
			});
		}

		//---------------- Controller logic ---------------------
		notificationService.getNotificationRules(function (results) {
			$scope.notificationRules = results.data;
		});
	}]);