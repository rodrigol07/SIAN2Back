using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SianApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            var cors = new EnableCorsAttribute(
                // origins: "http://172.21.200.40", //PRD
                // origins: "http://10.92.5.40", //QA
                origins: "http://localhost:4200", //DEV
                headers: "Origin, Content-Type, X-Requested-With",
                methods: "GET, POST, PUT, DELETE, OPTIONS");
            config.EnableCors(cors);

            config.Formatters.JsonFormatter
                .SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //config.Formatters.JsonFormatter
            //    .SerializerSettings
            //    .PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter
                .SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Formatters
                .Remove(config.Formatters.XmlFormatter);
        }
    }
}
