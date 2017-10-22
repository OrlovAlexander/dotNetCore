namespace EmployeeClient.Presentation.Common
{
    public class BasePresenter<TView> : IPresenter
        where TView : IView
    {
        protected readonly IApplicationController _applicationController;
        protected readonly IView _view;
        
        public BasePresenter(IApplicationController applicationController, IView view)
        {
            this._view = view ?? throw new System.ArgumentNullException(nameof(view));
            this._applicationController = applicationController ?? throw new System.ArgumentNullException(nameof(applicationController));
        }

        public void Run()
        {
            _view.Show();
        }
    }
}