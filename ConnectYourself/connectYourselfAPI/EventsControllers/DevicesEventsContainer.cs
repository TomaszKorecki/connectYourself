using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.Controllers.SignalR;
using connectYourselfAPI.EventsControllers.Models;

namespace connectYourselfAPI.EventsControllers {
	public class DevicesEventsContainer : IDevicesEventsContainer {

		public delegate void DeviceStateChangedEventHandler(DeviceStateChangedEvent deviceStateChangedEvent);

		private event DeviceStateChangedEventHandler OnDeviceStateChangedEvent;

		public void SubscribeToDeviceStateChangedEvent(DeviceStateChangedEventHandler func) {
			OnDeviceStateChangedEvent += func;
		}
		public void RegisterDeviceStateChangeEvent(DeviceStateChangedEvent deviceStateChangedEvent) {
			OnDeviceStateChangedEvent?.Invoke(deviceStateChangedEvent);
		}

		public void RegisterDeviceMessageEvent(DeviceMessageEvent deviceMessageEvent) {
		}
	}
}