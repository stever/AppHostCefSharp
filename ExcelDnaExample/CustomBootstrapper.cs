using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace ExcelDnaExample
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

        /// <summary>
        /// Root path is changed depending on build configuration.
        /// </summary>
        /// <see cref="http://github.com/NancyFx/Nancy/wiki/The-root-path"/>
        protected override IRootPathProvider RootPathProvider
            => new CustomRootPathProvider();

        /// <summary>
        /// Opportunity to intervene on a web request before it begins. This is
        /// useful for ensuring that only local requests are served, which is
        /// necessary to ensure an appropriate level of security.
        /// </summary>
        protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(requestContainer, pipelines, context);
            pipelines.BeforeRequest += ctx =>
            {
                // Only accept requests from local host.
                switch (ctx.Request.UserHostAddress)
                {
                    case "::1": // IPv6
                    case "127.0.0.1": // IPv4
                        return null;
                    default:
                        return new Response { StatusCode = HttpStatusCode.NotAcceptable };
                }
            };
        }
    }
}
