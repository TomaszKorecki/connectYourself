using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;

namespace connectYourselfAPI {
	public static class WebApiConfig {
		public static void Register(HttpConfiguration config) {

			//config.EnableCors();

			config.MapHttpAttributeRoutes();

			//config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

			if (config != null) {
				config.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { controller = "Default", method = "Get", id = RouteParameter.Optional }
					);

				//var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();

				config.Formatters.Clear();
				var jsonFormatter = new JsonMediaTypeFormatter();
				jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				config.Formatters.Add(jsonFormatter);
			}

			
		}
	}
}
