using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.Controllers.SignalR;
using connectYourselfAPI.DBContexts;
using connectYourselfAPI.DBContexts.EntityServices;
using connectYourselfAPI.EventsControllers.Models;
using connectYourselfAPI.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MongoDB.Driver;

namespace connectYourselfAPI.EventsControllers {
	public static class DevicesNotificationRulesHandler {

		public static void OnUserDeviceMessageReceived(DeviceMessageEvent deviceMessageEvent) {

			var notificationRulesService = new NotificationRuleService();
			var rules =
				notificationRulesService.Collection.Find(
					x => x.AppUserId == deviceMessageEvent.AppUserId && x.SourceDeviceId == deviceMessageEvent.DeviceId).ToList();

			if (rules.Any()) {
				var deviceService = new UserDeviceService();
				foreach (var notificationRule in rules) {
					if (notificationRule.SourceMessage == deviceMessageEvent.Message) {
						var device = deviceService.GetById(notificationRule.TargetDeviceId);
						if (device.ConnectionState == DeviveConnectionState.FullDuplex && !String.IsNullOrEmpty(device.ConnectionId)) {
							var devicesHub = GlobalHost.ConnectionManager.GetHubContext<DevicesHub>();
							devicesHub.Clients.Client(device.ConnectionId).NotificationRule(notificationRule.TargetMessage);
						}
					}
				}
			}
		}

	}
}