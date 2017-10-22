
using System;
using static System.Console;
using EmployeeClient.Model;
using EmployeeClient.Presentation.Views;

namespace EmployeeClient.UI
{
    public class MainView : IMainView
    {
        public MainView()
        {
        }

        public event Action<TaskEnum> ChosenTask;
        private void OnChosenTask(TaskEnum task)
        {
            if (ChosenTask != null)
                ChosenTask(task);
        }

        public void Show()
        {
            WriteLine("Выберете действие:");
            WriteLine(" 1. Добавить сотрудника");
            WriteLine(" 2. Показать всех сотрудников");
            WriteLine(" 3. Вывести сумму за месяц");
            string chosen = "";
            bool chosenValid = false;
            while(!chosenValid)
            {
                chosen = ReadLine();
                if (String.IsNullOrEmpty(chosen))
                {
                    WriteLine("Действие не выбрано. Смотри выше.");
                    continue;
                }
                if (String.IsNullOrWhiteSpace(chosen))
                {
                    WriteLine("Действие не выбрано. Смотри выше.");
                    continue;
                }
                int chosenValue = 0;
                if (Int32.TryParse(chosen, out chosenValue))
                {
                    if (chosenValue == 1)
                    {
                        OnChosenTask(TaskEnum.AddNewEmployee);
                        chosenValid = true;
                    }
                    if (chosenValue == 2)
                    {
                        OnChosenTask(TaskEnum.GetAllEmployees);
                        chosenValid = true;
                    }
                    if (chosenValue == 3)
                    {
                        OnChosenTask(TaskEnum.GetSumm);
                        chosenValid = true;
                    }
                }
                WriteLine("Действие выбрано не корректно.");
            }
        }
    }
}