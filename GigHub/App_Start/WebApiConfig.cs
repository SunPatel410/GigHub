using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace GigHub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //setting the api to use camel case notation when viewing on postman for readability,
            var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
