using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.Models;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts {
	public class UserDeviceService : EntityService<Device> {

		public List<Device> GetAllUserDevices(string userId) {
			return Collection.AsQueryable().Where(x => x.AppUserId == userId).ToList();
		} 
	}
}