using System;
using System.Linq.Expressions;
using Autofac;

namespace EmployeeClient.UI
{
    using EmployeeClient.Presentation.Common;

    public class AutofacAdapter : IContainer
    {
        private ContainerBuilder _containerBuilder;
        private Autofac.IContainer _scope;

        public AutofacAdapter()
        {
            _containerBuilder = new ContainerBuilder();
            _scope = null;
        }

        public void Register<TService, TImplementation>()
            where TImplementation : TService
        {
            _containerBuilder.RegisterType<TImplementation>()
            .AsSelf()
            .As<TService>();
        }

        public void Register<TService>()
        {
            _containerBuilder.RegisterType(typeof(TService));
        }

        public void RegisterInstance<TService, TImplementation>(TImplementation instance)
            where TImplementation : class, TService
        {
            _containerBuilder.RegisterInstance<TImplementation>(instance)
            .AsSelf()
            .As<TService>()
            .ExternallyOwned();
        }

        public TService Resolve<TService>()
            where TService : class
        {
            if (_scope == null)
                _scope = _containerBuilder.Build();

            return _scope.ResolveOptional<TService>();
        }
    }
}