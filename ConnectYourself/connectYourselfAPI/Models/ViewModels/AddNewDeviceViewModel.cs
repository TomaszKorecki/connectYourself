using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models {
	public class AddNewDeviceViewModel {
		[Required]
		public string Name { get; set; }
		public bool CahceData { get; set; }
	}
}