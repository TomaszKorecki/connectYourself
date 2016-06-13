using System;
using System.Collections.Generic;
using System.Configuration;
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
		private string ServerUrl { get; }


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
			//stockTickerHubProxy.On<Stock>("UpdateStockPrice", stock => Console.WriteLine("Stock update for {0} new price {1}", stock.Symbol, stock.Price));
			var hubConnection = new HubConnection(ServerUrl);
			IHubProxy devicesHub = hubConnection.CreateHubProxy(ServerHubName);
			await hubConnection.Start();

			await devicesHub.Invoke(ConnectMethod, new ConnectDeviceData() {
				DeviceName = DeviceName,
				SecretKey = SecretKey
			});
		}


    }
}
