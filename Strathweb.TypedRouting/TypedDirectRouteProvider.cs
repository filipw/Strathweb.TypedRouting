using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Strathweb.TypedRouting
{
    public class TypedDirectRouteProvider : DefaultDirectRouteProvider
    {
        internal static readonly Dictionary<Type, Dictionary<string, TypedRoute>> Routes = new Dictionary<Type, Dictionary<string, TypedRoute>>();

        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            var factories = base.GetActionRouteFactories(actionDescriptor).ToList();
            if (Routes.ContainsKey(actionDescriptor.ControllerDescriptor.ControllerType))
            {
                var controllerLevelDictionary = Routes[actionDescriptor.ControllerDescriptor.ControllerType];
                if (controllerLevelDictionary.ContainsKey(actionDescriptor.ActionName))
                {
                    factories.Add(controllerLevelDictionary[actionDescriptor.ActionName]);
                }
            }

            return factories;
        }
    }
}