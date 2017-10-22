using System;
using EmployeeClient.Model;
using EmployeeClient.Presentation.Common;
using EmployeeClient.Presentation.Views;

namespace EmployeeClient.Presentation.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        private readonly IMainView _mainView;

        public MainPresenter(IApplicationController applicationController, IMainView mainView)
            : base(applicationController, mainView)
        {
            _mainView = mainView ?? throw new System.ArgumentNullException(nameof(mainView));
            _mainView.ChosenTask += ChosenTask;
        }

        private void ChosenTask(TaskEnum task)
        {
            if (task == TaskEnum.AddNewEmployee)
            {
                _applicationController.Run<AddNewEmployeeTask>();
            }
            if (task == TaskEnum.GetAllEmployees)
            {
                _applicationController.Run<GetAllEmployeesTask>();
            }
            if (task == TaskEnum.GetSumm)
            {
                _applicationController.Run<GetSummTask>();
            }
        }
    }
}