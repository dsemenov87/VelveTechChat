using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTest.App_Start
{
    using Ninject;
    using Ninject.Web.Common;
    using Microsoft.Owin;
    using Owin;

    public partial class Startup
    {
        public IKernel ConfigureNinject(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var kernel = CreateKernel();
            app.UseNinjectMiddleware(() => kernel)
               .UseNinjectWebApi(config);

            return kernel;
        }

        public IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }

    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            kernel.Bind<IOAuthAuthorizationServerOptions>()
                .To<MyOAuthAuthorizationServerOptions>();
            kernel.Bind<IOAuthAuthorizationServerProvider>()
                .To<AuthorizationServerProvider>();
            kernel.Bind<IAuthenticationTokenProvider>().To<RefreshTokenProvider>();
            kernel.Bind<IUserService>().To<MyUserService>();
        }
    }
}