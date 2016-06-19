﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.Controllers.SignalR;
using connectYourselfAPI.DBContexts;
using connectYourselfAPI.EventsControllers.Models;
using Microsoft.AspNet.SignalR;

namespace connectYourselfAPI.EventsControllers {
	public static class UsersNotifier {

		public static Dictionary<string, UserFullDuplexConnection> UsersConnections =
			new Dictionary<string, UserFullDuplexConnection>();

		public static void OnUserDeviceStateChanged(DeviceStateChangedEvent deviceStateChangedEvent) {
			var context = GlobalHost.ConnectionManager.GetHubContext<UsersHub>();
			//context.Clients.All.Send("Admin", "stop the chat");

			if (UsersConnections.ContainsKey(deviceStateChangedEvent.AppUserId)) {
				
				UserDeviceService userDeviceService = new UserDeviceService();
				var device = userDeviceService.GetById(deviceStateChangedEvent.DeviceId);

				if (device != null) {
					var connection = UsersConnections[deviceStateChangedEvent.AppUserId];
					context.Clients.Client(connection.ConnectionId).NotifyAboutDeviceStateChange(new DeviceStateChangedNotification() {
						DateTime = deviceStateChangedEvent.DateTime,
						State = deviceStateChangedEvent.State,
						DeviceName = device.Name
					});
				}
			}
		}
	}
}