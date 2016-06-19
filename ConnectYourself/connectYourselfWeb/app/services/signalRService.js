app.factory('signalRService', ['$rootScope', 'signalRUrl',
  function ($rootScope, signalRUrl) {

  	var signalRServiceFactory = {};
  	var proxy = null;

  	var connect = function (hubName, onConnectedCallback) {
  		console.log("Instantiate signalR service");
  		var connection = $.hubConnection(signalRUrl);
  		proxy = connection.createHubProxy(hubName);
  		signalRServiceFactory.proxy = proxy;

  		connection.start().done(function () {
  			if (onConnectedCallback)
  				onConnectedCallback();
  		});

  	}

  	var on = function (eventName, callback) {
  		proxy.on(eventName, function (result) {
  			$rootScope.$apply(function () {
  				if (callback) {
  					callback(result);
  				}
  			});
  		});

		console.log("Registered method: " + eventName);
  	}

  	var invoke = function (methodName, params, callback) {
  		proxy.invoke(methodName, params)
			  .done(function (result) {
			  	$rootScope.$apply(function () {
			  		if (callback) {
			  			callback(result);
			  		}
			  	});
			  });
  	}

  	//function backendFactory(hubName, onConnectedCallback) {
  	//	var connection = $.hubConnection(signalRUrl);
  	//	var proxy = connection.createHubProxy(hubName);

  	//	connection.start().done(function () {
  	//		if (onConnectedCallback)
  	//			onConnectedCallback();
  	//	});

  	//	return {
  	//		on: function (eventName, callback) {
  	//			proxy.on(eventName, function (result) {
  	//				$rootScope.$apply(function () {
  	//					if (callback) {
  	//						callback(result);
  	//					}
  	//				});
  	//			});
  	//		},
  	//		invoke: function (methodName, params, callback) {
  	//			proxy.invoke(methodName, params)
  	//			.done(function (result) {
  	//				$rootScope.$apply(function () {
  	//					if (callback) {
  	//						callback(result);
  	//					}
  	//				});
  	//			});
  	//		}
  	//	};
  	//};
  	//backendFactory();

  	signalRServiceFactory.connect = connect;
  	signalRServiceFactory.on = on;
  	signalRServiceFactory.invoke = invoke;
  	return signalRServiceFactory;
  }]);