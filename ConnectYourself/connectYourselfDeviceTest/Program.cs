using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConnectYourselfClient;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;

namespace connectYourselfDeviceTest {
	class Program {

		static void Main(string[] args) {
			Console.WriteLine("Device program has started");

			Console.WriteLine("Attempt to connect to server");

			CYClient connectYouselfClient;
			try {
				connectYouselfClient = new CYClient("Sample1", "c6bef65cd9cb48b0b98503bb1e6270a6", "http://localhost:55932/signalR");
				Console.WriteLine("Registered on server...");

				Random random = new Random();
				Task.Run(async () => {
					while (true) {
						var temperature = string.Format("Temperature: {0}", random.Next(35));
						Console.WriteLine(temperature);
						await connectYouselfClient.SendMessageToServer(temperature);

						await Task.Delay(random.Next(3000, 6000));
					}
				});
			}
			catch (Exception exception) {
				Console.WriteLine("Problem occured duing connecting to server");
			}

			Console.ReadLine();
		}

		//static Task SendRandomNumberToServer() {

		//}
	}
}
