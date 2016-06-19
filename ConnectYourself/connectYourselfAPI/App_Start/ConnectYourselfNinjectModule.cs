using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.EventsControllers;
using Ninject.Modules;

namespace connectYourselfAPI.App_Start {
	public class ConnectYourselfNinjectModule : NinjectModule {
		public override void Load() {
			Bind<IDevicesEventsContainer>().ToConstructor(x => new DevicesEventsContainer()).InSingletonScope();
		}
	}
}