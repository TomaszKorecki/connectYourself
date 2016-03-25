'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings', function ($scope, $location, authService, ngAuthSettings) {

	$scope.loginData = {
		userName: "",
		password: "",
	};

	$scope.message = "";

	$scope.login = function () {

		authService.login($scope.loginData).then(function (response) {

			$location.path('/home');

		},
         function (err) {
         	$scope.message = err.error_description;
         });
	};

	$scope.authCompletedCB = function (fragment) {

		$scope.$apply(function () {

			//Obtain access token and redirect to orders
			var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
			authService.obtainAccessToken(externalData).then(function (response) {

				$location.path('/home');

			},
		 function (err) {
		 	$scope.message = err.error_description;
		 });

		});
	}
}]);
