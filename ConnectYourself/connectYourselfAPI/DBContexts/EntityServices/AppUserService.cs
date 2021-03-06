﻿using System.Configuration;
using connectYourselfAPI.DBContexts.EntityServices;
using connectYourselfAPI.Models;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts {
	public class AppUserService : EntityService<AppUser> {
		//Additional methods for AppUser model

		public AppUser GetUserByUsername(string username) {
			return Collection.Find(x => x.UserName == username).FirstOrDefault();
		}

	}
}