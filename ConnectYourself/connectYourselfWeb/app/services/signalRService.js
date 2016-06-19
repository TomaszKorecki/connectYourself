app.factory('signalRService', ['$rootScope', 'signalRUrl',
  function ($rootScope, signalRUrl) {

  	function backendFactory(hubName, onConnectedCallback) {
  		var connection = $.hubConnection(signalRUrl);
  		var proxy = connection.createHubProxy(hubName);

  		connection.start().done(function () {
  			if (onConnectedCallback)
  				onConnectedCallback();
  		});

  		return {
  			on: function (eventName, callback) {
  				proxy.on(eventName, function (result) {
  					$rootScope.$apply(function () {
  						if (callback) {
  							callback(result);
  						}
  					});
  				});
  			},
  			invoke: function (methodName, params, callback) {
  				proxy.invoke(methodName, params)
				.done(function (result) {
					$rootScope.$apply(function () {
						if (callback) {
							callback(result);
						}
					});
				});
  			}
  		};
  	};

  	return backendFactory;
  }]);