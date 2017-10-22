using System;
using System.Linq.Expressions;

namespace EmployeeClient.Presentation.Common
{
    public interface IContainer
    {
        void Register<TService>();

        void Register<TService, TImplementation>()
            where TImplementation : TService;

        void RegisterInstance<TService, TImplementation>(TImplementation instance)
            where TImplementation : class, TService;

        TService Resolve<TService>()
            where TService : class;
    }
}