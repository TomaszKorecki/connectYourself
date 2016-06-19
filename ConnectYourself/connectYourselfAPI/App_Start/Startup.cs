using System;
using System.Web.Http;
using connectYourselfAPI.AuthProviders;
using connectYourselfAPI.EventsControllers;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(connectYourselfAPI.App_Start.Startup))]
namespace connectYourselfAPI.App_Start {
	public class Startup {
		public void Configuration(IAppBuilder app) {
			ConfigureOAuth(app);

			HttpConfiguration config = new HttpConfiguration();
			WebApiConfig.Register(config);

			//For allowing getting token from another apps
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);

			app.MapSignalR("/signalR", new HubConfiguration());

			IKernel kernel = new StandardKernel(new ConnectYourselfNinjectModule());

			var devicesEventsContainer = kernel.Get<IDevicesEventsContainer>();
			devicesEventsContainer.SubscribeToDeviceStateChangedEvent(UsersNotifier.OnUserDeviceStateChanged);
		}

		public void ConfigureOAuth(IAppBuilder app) {
			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions() {
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new SimpleAuthorizationServerProvider(),
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

		}
	}
}