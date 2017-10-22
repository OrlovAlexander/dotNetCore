using EmployeeClient.Presentation.Views;

namespace EmployeeClient.Presentation.Common
{
    public class ApplicationController : IApplicationController
    {
        private readonly IContainer _container;

        public ApplicationController(IContainer container)
        {
            _container = container;
            _container.RegisterInstance<IApplicationController, ApplicationController>(this);
        }

        public IApplicationController RegisterService<TService, TImplementation>()
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>();
            return this;
        }

        public IApplicationController RegisterView<TView, TImplementation>()
            where TImplementation : class, TView
            where TView : IView
        {
            _container.Register<TView, TImplementation>();
            return this;
        }

        public void Run<TPresenter>()
            where TPresenter : class, IPresenter
        {
            TPresenter presenter = _container.Resolve<TPresenter>();
            presenter.Run();
        }

        public void Run<TPresenter, TArgumnent>(TArgumnent argumnent)
            where TPresenter : class, IPresenter<TArgumnent>
        {
            TPresenter presenter = _container.Resolve<TPresenter>();
            presenter.Run(argumnent);
        }
    }
}