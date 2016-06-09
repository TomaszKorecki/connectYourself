using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using connectYourselfAPI.DBContexts;
using connectYourselfAPI.DBContexts.EntityServices;
using connectYourselfAPI.Models;
using connectYourselfAPI.Models.SignalRModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Messaging;

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
			}
		}

		public void SetDeviceState(SetDeviceStateData setDeviceStateData) {
			UserDeviceService.UpdateDeviceState(setDeviceStateData.SecretKey, setDeviceStateData.DeviceState);
		}

		public void SendDeviceData(SendDeviceDataData sendDeviceData) {

			var device = UserDeviceService.GetBySecretKey(sendDeviceData.SecretKey);
			if (device != null) {
				DeviceMessageService deviceMessageService = new DeviceMessageService();

				var deviceMessage = new DeviceMessage() {
					DeviceId = device.Id,
					MessageContent = sendDeviceData.Data
				};


				deviceMessageService.Collection.InsertOne(deviceMessage);
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