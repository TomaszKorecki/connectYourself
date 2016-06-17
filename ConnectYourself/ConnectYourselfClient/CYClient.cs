using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using connectYourselfLib.Models;
using Microsoft.AspNet.SignalR.Client;
using static System.String;

namespace ConnectYourselfClient
{
    public class CYClient {
	    private const string ServerUrlConfigName = "ConnectYourselfServerUrl";
	    private const string ServerHubName = "DevicesHub";
	    private const string ConnectMethod = "ConnectDevice";
	    private const string SendMessageMethod = "SendDeviceData";
		private string ServerUrl { get; }

		private IHubProxy HubProxy { get; set; }

		public string DeviceName { get; }
		public string SecretKey { get; }

		
	    public CYClient(string deviceName, string secretKey, string serverUrl = null) {
		    if (IsNullOrEmpty(serverUrl)) {
			    ServerUrl = ConfigurationManager.AppSettings[ServerUrlConfigName];
			    if (IsNullOrEmpty(ServerUrl)) {
				    throw (new Exception("There is no ConnectYourselfServerUrl parameter in app config"));
			    }
		    } else {
			    ServerUrl = serverUrl;
		    }

		    DeviceName = deviceName;
		    SecretKey = secretKey;

		    ConnectToServer().Wait();
	    }

	    public async Task ConnectToServer() {
			var hubConnection = new HubConnection(ServerUrl);
			HubProxy = hubConnection.CreateHubProxy(ServerHubName);
			await hubConnection.Start();

			await HubProxy.Invoke(ConnectMethod, new ConnectDeviceData() {
				DeviceName = DeviceName,
				SecretKey = SecretKey
			});
		}

	    public async Task SendMessageToServer(string message) {
			await HubProxy.Invoke(SendMessageMethod, new SendDeviceData() {
				DeviceName = DeviceName,
				SecretKey = SecretKey,
				Data = message
			});
		}
    }
}
