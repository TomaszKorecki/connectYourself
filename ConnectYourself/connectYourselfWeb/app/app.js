
var app = angular.module('ConnectYourselfApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'toaster', 'ngAnimate']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/device/:device_id", {
    	controller: "deviceController",
    	templateUrl: "/app/views/deviceDetailView.html"
    });

    $routeProvider.when("/notifications/", {
    	controller: "notificationsController",
    	templateUrl: "/app/views/notificationsView.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

var serviceBase = 'http://localhost:55932/';
app.constant('ngAuthSettings', {

    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});
app.value('signalRUrl', 'http://localhost:55932/signalR');

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
	authService.fillAuthData();

	//signalRService.connect('UsersHub', function () {
	//	signalRService.invoke("RegisterUser", authService.authentication.userName);
	//});
}]);

