﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNet.Identity.MongoDB;

namespace connectYourselfAPI.Models {
	public class AppUser : IdentityUser, IMongoEntity {
		string IMongoEntity.Id {
			get {
				return base.Id;
			}
			set {

			}
		}
	}
}