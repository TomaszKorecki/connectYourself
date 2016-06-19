using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using connectYourselfAPI.App_Start;
using connectYourselfAPI.DBContexts;
using connectYourselfAPI.DBContexts.EntityServices;
using connectYourselfAPI.EventsControllers;
using connectYourselfAPI.EventsControllers.Models;
using connectYourselfAPI.Models;
using connectYourselfAPI.Models.DBModels;
using connectYourselfLib.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Messaging;
using Ninject;

namespace connectYourselfAPI.Controllers.SignalR {
	public class DevicesHub : Hub {

		private UserDeviceService UserDeviceService { get; set; }

		public DevicesHub() {
			UserDeviceService = new UserDeviceService();
		}

		public void ConnectDevice(ConnectDeviceData deviceData) {
			var device = UserDeviceService.GetBySecretKey(deviceData.SecretKey);
			if (device != null) {
				device.ConnectionState = DeviveConnectionState.FullDuplex;
				device.ConnectionId = Context.ConnectionId;
				UserDeviceService.Update(device);
			}
		}

		public void DisconnectDevice(DisconnectDeviceData disconnectDeviceData) {
			var device = UserDeviceService.GetBySecretKey(disconnectDeviceData.SecretKey);
			if (device != null) {
				device.ConnectionState = DeviveConnectionState.NotConnected;
				device.ConnectionId = string.Empty;

				UserDeviceService.Update(device);
			}
		}

		public void SetDeviceState(SetDeviceStateData setDeviceStateData) {
			var device = UserDeviceService.GetBySecretKey(setDeviceStateData.SecretKey);

			if (device != null) {
				DeviceHistoricalState deviceHistoricalState = new DeviceHistoricalState() {
					State = device.ActualState,
					StateTransitionDateTime = DateTime.Now,
					DeviceId = device.Id
				};

				UserDeviceService.SetupFreshLastPing(device);
				if (UserDeviceService.UpdateDeviceState(device, setDeviceStateData.DeviceState)) {
					var historialStateES = new EntityService<DeviceHistoricalState>();
					historialStateES.Create(deviceHistoricalState);
				}

				IKernel kernel = new StandardKernel(new ConnectYourselfNinjectModule());
				var deviceEventsContainer = kernel.Get<IDevicesEventsContainer>();

				deviceEventsContainer.RegisterDeviceStateChangeEvent(new DeviceStateChangedEvent {
					DeviceId = device.Id,
					DateTime = deviceHistoricalState.StateTransitionDateTime,
					State = setDeviceStateData.DeviceState,
					AppUserId = device.AppUserId
				});
			}
		}

		public void SendDeviceData(SendDeviceData sendDeviceData) {
			var device = UserDeviceService.GetBySecretKey(sendDeviceData.SecretKey);
			if (device != null) {
				UserDeviceService.SetupFreshLastPing(device);
				UserDeviceService.Update(device);

				DeviceMessageService deviceMessageService = new DeviceMessageService();

				var deviceMessage = new DeviceMessage() {
					DeviceId = device.Id,
					MessageContent = sendDeviceData.Data,
					MessageDateTime = DateTime.Now
				};

				deviceMessageService.Create(deviceMessage);

				IKernel kernel = new StandardKernel(new ConnectYourselfNinjectModule());
				var deviceEventsContainer = kernel.Get<IDevicesEventsContainer>();

				deviceEventsContainer.RegisterDeviceMessageEvent(new DeviceMessageEvent() {
					DeviceId = device.Id,
					DateTime = deviceMessage.MessageDateTime,
					Message = deviceMessage.MessageContent,
					AppUserId = device.AppUserId,
					DeviceName = device.Name
				});
			}
		}

		public void DevicePing(DevicePing devicePing) {
			UserDeviceService.UpdateDeviceLastPing(devicePing.SecretKey, DateTime.Now);
		}

		public override Task OnDisconnected(bool stopCalled) {
			var device = UserDeviceService.GetByConnectionId(Context.ConnectionId);
			if (device != null) {
				device.ConnectionState = DeviveConnectionState.NotConnected;
				device.ConnectionId = string.Empty;
				UserDeviceService.Update(device);
			}

			return base.OnDisconnected(stopCalled);
		}
	}
}