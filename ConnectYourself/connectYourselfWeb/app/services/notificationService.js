'use strict';
app.factory('notificationService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

	var serviceBaseUri = ngAuthSettings.apiServiceBaseUri;

	var notificationServiceFactory = {};


	var getNotificationRules = function (successCallback) {
		return $http.get(serviceBaseUri + 'api/notifications').then(function (results) {
			successCallback(results);
		});
	};

	var addNewNotificationRule = function (newNotificationRuleData, successCallback, errorCallback) {
		return $http.post(serviceBaseUri + 'api/notifications', newNotificationRuleData).then(function (results) {
			successCallback(results);
		}, function(error) {
			errorCallback(error);
		});
	}

	var onRemoveNotificationRule = function (notificationRule, successCallback, errorCallback) {
		$http.delete(serviceBaseUri + 'api/notifications/' + notificationRule.id).then(function (result) {
			successCallback(result);
		}, function (error) {
			errorCallback(error);
		});
	}

	notificationServiceFactory.getNotificationRules = getNotificationRules;
	notificationServiceFactory.onAddNewNotificationRule = addNewNotificationRule;
	notificationServiceFactory.onRemoveNotificationRule = onRemoveNotificationRule;

	return notificationServiceFactory;
}]);