using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Strathweb.TypedRouting
{
    public class TypedRoute : IDirectRouteFactory
    {
        public TypedRoute(string template)
        {
            Template = template;
        }

        public Type ControllerType { get; private set; }

        public string RouteName { get; private set; }

        public string Template { get; private set; }

        public string ControllerName
        {
            get { return ControllerType != null ? ControllerType.FullName : string.Empty; }
        }

        public string ActionName { get; private set; }

        public MethodInfo ActionMember { get; private set; }

        RouteEntry IDirectRouteFactory.CreateRoute(DirectRouteFactoryContext context)
        {
            IDirectRouteBuilder builder = context.CreateBuilder(Template);

            builder.Name = RouteName;
            return builder.Build();
        }

        public TypedRoute Controller<TController>() where TController : IHttpController
        {
            ControllerType = typeof(TController);
            return this;
        }

        public TypedRoute Action<T, U>(Expression<Func<T, U>> expression)
        {
            ActionMember = GetMethodInfoInternal(expression);
            ControllerType = ActionMember.DeclaringType;
            ActionName = ActionMember.Name;
            return this;
        }

        public TypedRoute Action<T>(Expression<Action<T>> expression)
        {
            ActionMember = GetMethodInfoInternal(expression);
            ControllerType = ActionMember.DeclaringType;
            ActionName = ActionMember.Name;
            return this;
        }

        private static MethodInfo GetMethodInfoInternal(dynamic expression)
        {
            var method = expression.Body as MethodCallExpression;
            if (method != null)
                return method.Method;

            throw new ArgumentException("Expression is incorrect!");
        }

        public TypedRoute Name(string name)
        {
            RouteName = name;
            return this;
        }

        public TypedRoute Action(string actionName)
        {
            ActionName = actionName;
            return this;
        }
    }
}