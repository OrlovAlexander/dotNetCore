using System;
using EmployeeClient.Model;
using EmployeeClient.Presentation.Common;
using EmployeeClient.Presentation.Views;

namespace EmployeeClient.Presentation.Presenters
{
    internal class GetAllEmployeesTask : BasePresenter<IGetAllEmployeesTaskView>
    {
        private readonly IGetAllEmployeesTaskView getAllEmployeesTaskView;
        
        public GetAllEmployeesTask(IApplicationController applicationController, IGetAllEmployeesTaskView getAllEmployeesTaskView)
            : base(applicationController, getAllEmployeesTaskView)
        {
            this.getAllEmployeesTaskView = getAllEmployeesTaskView ?? throw new ArgumentNullException(nameof(getAllEmployeesTaskView));
        }
    }
}