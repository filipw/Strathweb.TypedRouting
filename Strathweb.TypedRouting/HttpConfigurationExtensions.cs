using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Strathweb.TypedRouting
{
    public static class HttpConfigurationExtensions
    {
        public static TypedRoute TypedRoute(this HttpConfiguration config, string template, Action<TypedRoute> configSetup)
        {
            var route = new TypedRoute(template);
            configSetup(route);

            if (TypedDirectRouteProvider.Routes.ContainsKey(route.ControllerType))
            {
                var controllerLevelDictionary = TypedDirectRouteProvider.Routes[route.ControllerType];
                controllerLevelDictionary.Add(route.ActionName, route);
            }
            else
            {
                var controllerLevelDictionary = new Dictionary<string, TypedRoute> { { route.ActionName, route } };
                TypedDirectRouteProvider.Routes.Add(route.ControllerType, controllerLevelDictionary);
            }

            return route;
        }

        public static void EnableTypedRouting(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(new TypedDirectRouteProvider());
        }
    }
}
