using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using connectYourselfAPI.EventsControllers.Models;

namespace connectYourselfAPI.EventsControllers {
	interface IDevicesEventsContainer {

		void SubscribeToDeviceStateChangedEvent(DevicesEventsContainer.DeviceStateChangedEventHandler func);
		void RegisterDeviceStateChangeEvent(DeviceStateChangedEvent deviceStateChangedEvent);
		void RegisterDeviceMessageEvent(DeviceMessageEvent deviceMessageEvent);
	}
}
