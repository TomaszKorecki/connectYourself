using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.DBContexts;
using connectYourselfAPI.Models;
using connectYourselfAPI.Models.SignalRModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Messaging;

namespace connectYourselfAPI.Controllers.SignalR {
	public class DevicesHub : Hub {

		private UserDeviceService UserDeviceService { get; set; }

		public void ConnectDevice(ConnectDeviceData deviceData) {
			UserDeviceService = new UserDeviceService();
			var device = UserDeviceService.GetBySecretKey(deviceData.SecretKey);
			if (device != null) {
				device.ConnectionState = DeviveConnectionState.FullDuplex;
			}
		}

		public void DisconnectDevice(DisconnectDeviceDataData disconnectDeviceData) {
			
		}

		public void SetDeviceState(SetDeviceStateData setDeviceStateData) {
			
		}

		public void SendDeviceData(SendDeviceDataData sendDeviceData) {
			
		}

		public void DevicePing(DevicePing devicePing) {
			
		}
	}
}