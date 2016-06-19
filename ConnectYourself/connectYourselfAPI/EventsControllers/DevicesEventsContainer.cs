using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.Controllers.SignalR;
using connectYourselfAPI.EventsControllers.Models;

namespace connectYourselfAPI.EventsControllers {
	public class DevicesEventsContainer : IDevicesEventsContainer {

		public DevicesEventsContainer() {
			SubscribeToDeviceStateChangedEvent(UsersNotifier.OnUserDeviceStateChanged);
			SubscribeToDeviceMessageReceivedEvent(UsersNotifier.OnUserDeviceMessageReceived);
		}

		
		public delegate void DeviceStateChangedEventHandler(DeviceStateChangedEvent deviceStateChangedEvent);
		public delegate void DeviceMessageReceivedEventHandler(DeviceMessageEvent deviceMessageEvent);

		private event DeviceStateChangedEventHandler OnDeviceStateChangedEvent;
		private event DeviceMessageReceivedEventHandler OnDeviceMessageReceived;

		public void SubscribeToDeviceStateChangedEvent(DeviceStateChangedEventHandler func) {
			OnDeviceStateChangedEvent += func;
		}

		public void SubscribeToDeviceMessageReceivedEvent(DeviceMessageReceivedEventHandler func) {
			OnDeviceMessageReceived += func;
		}

		public void RegisterDeviceStateChangeEvent(DeviceStateChangedEvent deviceStateChangedEvent) {
			OnDeviceStateChangedEvent?.Invoke(deviceStateChangedEvent);
		}

		public void RegisterDeviceMessageEvent(DeviceMessageEvent deviceMessageEvent) {
			OnDeviceMessageReceived?.Invoke(deviceMessageEvent);
		}
	}
}