using System;
using Model.Enums;

namespace Model
{
    public class EmployeeHourlyPay : Employee
    {
        private double _hourlyRate;
        public double HourlyRate { get { return _hourlyRate; } }

        public EmployeeHourlyPay(int id, string name, string firstName, string patronymic, double hourlyRate)
            : base (id, name, firstName, patronymic)
        {
            _hourlyRate = hourlyRate;
        }

        public override double AverageMonthlyWage()
        {
            return 20.8 * 8.0 * _hourlyRate;
        }

        public override EmployeeSalaryType SalaryType { get {return EmployeeSalaryType.HourlyPay; } }
    }
}