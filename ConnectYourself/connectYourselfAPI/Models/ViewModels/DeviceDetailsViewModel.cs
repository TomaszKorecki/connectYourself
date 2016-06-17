using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace connectYourselfAPI.Models.ViewModels {
	public class DeviceDetailsViewModel {
		public string Id { get; set; }
		public string Name { get; set; }
		public string SecretKey { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public DeviveConnectionState ConnectionState { get; set; }
		public DateTime LastPing { get; set; }
		public string ActualState { get; set; }
	}
}