using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using connectYourselfAPI.App_Start;
using connectYourselfAPI.DBContexts;
using connectYourselfAPI.EventsControllers;
using connectYourselfAPI.EventsControllers.Models;
using Microsoft.AspNet.SignalR;
using Ninject;

namespace connectYourselfAPI.Controllers.SignalR {
	public class UsersHub : Hub {

		public void RegisterUser(string username) {
			AppUserService appUserService = new AppUserService();
			var appUser = appUserService.GetUserByUsername(username);

			if (appUser != null) {
				var connectionId = Context.ConnectionId;

				if (UsersNotifier.UsersConnections.ContainsKey(appUser.Id)) {
					UsersNotifier.UsersConnections[appUser.Id] = new UserFullDuplexConnection {ConnectionId = connectionId};
				} else {
					UsersNotifier.UsersConnections.Add(appUser.Id, new UserFullDuplexConnection {ConnectionId = connectionId});
				}
			}
		}

		public override Task OnDisconnected(bool stopCalled) {
			var connectionId = Context.ConnectionId;

			var userConnection = UsersNotifier.UsersConnections.FirstOrDefault(x => x.Value.ConnectionId == connectionId);
			if (userConnection.Value != null) {
				UsersNotifier.UsersConnections.Remove(userConnection.Key);
			}

			return base.OnDisconnected(stopCalled);
		}
	}
}