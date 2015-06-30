using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using Ninject.Infrastructure; 


namespace WebAppTest
{
    public abstract class NinjectHttpApplication : HttpApplication, IHaveKernel
    {
        protected NinjectHttpApplication();

        public void Application_End();

        public void Application_Start();

        protected abstract IKernel CreateKernel();

        public override void Init();

        protected virtual void OnApplicationStarted();

        protected virtual void OnApplicationStopped();
    }
}