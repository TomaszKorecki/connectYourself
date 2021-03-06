﻿'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

	var apiServiceBaseUri = ngAuthSettings.apiServiceBaseUri;
	var authServiceFactory = {};

	var _authentication = {
		isAuth: false,
		userName: ""
	};

	var _saveRegistration = function (registration) {

		_logOut();

		return $http.post(apiServiceBaseUri + 'api/account/register', registration).then(function (response) {
			return response;
		});

	};

	var _login = function (loginData) {

		var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

		var deferred = $q.defer();

		$http.post(apiServiceBaseUri + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

			localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });
			_authentication.isAuth = true;
			_authentication.userName = loginData.userName;

			deferred.resolve(response);

		}).error(function (err, status) {
			_logOut();
			deferred.reject(err);
		});

		return deferred.promise;

	};

	var _logOut = function () {

		localStorageService.remove('authorizationData');

		_authentication.isAuth = false;
		_authentication.userName = "";
	};

	var _fillAuthData = function () {

		var authData = localStorageService.get('authorizationData');
		if (authData) {
			_authentication.isAuth = true;
			_authentication.userName = authData.userName;
		}
	};

	var _obtainAccessToken = function (externalData) {

		var deferred = $q.defer();

		$http.get(apiServiceBaseUri + 'api/account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).success(function (response) {

			localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName});

			_authentication.isAuth = true;
			_authentication.userName = response.userName;

			deferred.resolve(response);

		}).error(function (err, status) {
			_logOut();
			deferred.reject(err);
		});

		return deferred.promise;

	};


	authServiceFactory.saveRegistration = _saveRegistration;
	authServiceFactory.login = _login;
	authServiceFactory.logOut = _logOut;
	authServiceFactory.fillAuthData = _fillAuthData;
	authServiceFactory.authentication = _authentication;

	authServiceFactory.obtainAccessToken = _obtainAccessToken;

	return authServiceFactory;
}]);