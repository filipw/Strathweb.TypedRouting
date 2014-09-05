Strathweb.TypedRouting
======================

A library allowing you to define Web API 2 direct routes using strongly typed, centralized syntax. This allows you to ensure tht whenever you refactor your application, the routes will not break.

### Installation and requirements

`TypedRouting` works only with Web API 2.2+. The abstractions used to build up the library were only introduced in the 2.2 release of the framework. Internally, `TypedRouting` is an implementation of `IDirectRouteProvider` - same mechanism as used by attribute routing.

You can grab the package from Nuget:

    install-package Strathweb.TypedRouting

### Usage example

Consider the sample controller:

    public class TestController : ApiController 
    {
        public HttpResponseMessage Get()
        {
          //omitted for brevity
        }
        
        public HttpResponseMessage GetById(int id)
        {
          //omitted for brevity
        }
    }

You define the routes to this controller with `TypedRouting` using the following syntax:

    var config = new HttpConfiguration();
    config.EnableTypedRouting();
 
    config.TypedRoute("test", c => c.Action<TestController>(x => x.Get()));
    config.TypedRoute("test/{id:int}", c => c.Action<TestController>(x => x.GetById(Param.Any<int>())));

You can use the fluent API to give the route a name (so that you can use it with `UrlHelper` and other libraries requiring you to reference routes by name:

    config.TypedRoute("test", c => c.Action<TestController>(x => x.Get().Name("getTest"));
    
`TypedRouting` works well with `UrlHelper` and with [Drum](https://github.com/pmhsfelix/drum)

### Blog post

[Read more](http://www.strathweb.com/2014/07/building-strongly-typed-route-provider-asp-net-web-api/) at my blog

### License

2014 Filip W [MIT](https://github.com/filipw/Strathweb.TypedRouting/blob/master/LICENSE)
