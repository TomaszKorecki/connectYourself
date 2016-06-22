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

			SimulateDevice1();
			SimulateDevice2();

			Console.ReadLine();
		}

		static void SimulateDevice1() {
			CYClient connectYouselfClient;
			try {
				connectYouselfClient = new CYClient("Kitchen termometer", "75c2ea8481cc48f98a63d6e211b2eb05", "http://localhost:55932/signalR");
				Console.WriteLine("Device 1 Registered on server...");

				connectYouselfClient.HubProxy.On("NotificationRule", (msg) => {
					var response = String.Format("Received message from notification rule: {0}", msg);
					Console.WriteLine(response);
					connectYouselfClient.SendMessageToServer(response);
				});

				Random random = new Random();
				Task.Run(async () => {
					while (true) {
						var temperature = string.Format("Temperature: {0}", random.Next(35));
						Console.WriteLine(temperature);
						connectYouselfClient.SendMessageToServer(temperature);
						await Task.Delay(random.Next(3000, 6000));
					}
				});

				Task.Run(async () => {
					while (true) {
						var newState = random.Next(0, 2) > 0 ? "Turning ON" : "Turning OFF";
						Console.WriteLine("Setting up new state: {0}", newState);
						connectYouselfClient.SetNewState(newState);
						Console.WriteLine("State set up properly");
						await Task.Delay(random.Next(10000, 15000));
					}
				});

			} catch (Exception exception) {
				Console.WriteLine("Problem occured duing connecting to server");
			}
		}

		static void SimulateDevice2() {
			CYClient connectYouselfClient;
			try {
				connectYouselfClient = new CYClient("Outside lamp", "88070692154945899ec7a6b22456a19b", "http://localhost:55932/signalR");
				Console.WriteLine("Device 2 Registered on server...");

				Random random = new Random();
				Task.Run(async () => {
					while (true) {
						var message = "Hello to device termometer";
						Console.WriteLine(message);
						connectYouselfClient.SendMessageToServer(message);
						await Task.Delay(random.Next(3000, 6000));
					}
				});

			} catch (Exception exception) {
				Console.WriteLine("Problem occured duing connecting to server");
			}
		}
	}
}
