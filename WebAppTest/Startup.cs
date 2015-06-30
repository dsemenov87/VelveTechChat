using Microsoft.Owin;
using Owin;
using Ninject;
using Microsoft.AspNet.SignalR;

using VelveTechChat.Models;
using VelveTechChat.Models.DAL;

[assembly: OwinStartupAttribute(typeof(VelveTechChat.Startup))]
namespace VelveTechChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            var kernel = new StandardKernel();
            var resolver = new NinjectSignalRDependencyResolver(kernel);

            kernel.Bind<IRepository>().To<SqlRepository>().InTransientScope(); 

            var config = new HubConfiguration();
            config.Resolver = resolver;
            app.MapSignalR(config);
        }
    }
}
