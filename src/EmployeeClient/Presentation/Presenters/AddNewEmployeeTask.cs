using System;
using EmployeeClient.Model;
using EmployeeClient.Presentation.Common;
using EmployeeClient.Presentation.Views;

namespace EmployeeClient.Presentation.Presenters
{
    internal class AddNewEmployeeTask : BasePresenter<IAddNewEmployeeTaskView>
    {
        private readonly IAddNewEmployeeTaskView _addNewEmployeeTaskView;

        public AddNewEmployeeTask(IApplicationController applicationController, IAddNewEmployeeTaskView addNewEmployeeTaskView)
            : base(applicationController, addNewEmployeeTaskView)
        {
            this._addNewEmployeeTaskView = addNewEmployeeTaskView ?? throw new ArgumentNullException(nameof(addNewEmployeeTaskView));
        }
    }
}