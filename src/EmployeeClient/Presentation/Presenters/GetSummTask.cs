using System;
using EmployeeClient.Model;
using EmployeeClient.Presentation.Common;
using EmployeeClient.Presentation.Views;

namespace EmployeeClient.Presentation.Presenters
{
    internal class GetSummTask : BasePresenter<IGetSummTaskView>
    {
        private readonly IGetSummTaskView getSummTaskView;
        public GetSummTask(IApplicationController applicationController, IGetSummTaskView getSummTaskView)
            : base(applicationController, getSummTaskView)
        {
            this.getSummTaskView = getSummTaskView ?? throw new ArgumentNullException(nameof(getSummTaskView));
        }
    }
}