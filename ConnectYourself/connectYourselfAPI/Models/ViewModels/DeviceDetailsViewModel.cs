using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models.ViewModels {
	public class DeviceDetailsViewModel {

		public string Id { get; set; }
		public string Name { get; set; }
		public string SecretKey { get; set; }
		public DeviveConnectionState ConnectionState { get; set; }
		public DateTime LastPing { get; set; }
		public string ActualState { get; set; }
	}
}