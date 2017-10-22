using System;
using EmployeeClient.Model;
using EmployeeClient.Presentation.Common;

namespace EmployeeClient.Presentation.Views
{
    public interface IMainView : IView
    {
        event Action<TaskEnum> ChosenTask;
    }
}