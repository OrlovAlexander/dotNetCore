using System;
using Model.Enums;

namespace Model
{
    public class EmployeeSalary : Employee
    {
        private double _fixedRate;
        public double SalaryRate { get { return _fixedRate; } }

        public EmployeeSalary(int id, string name, string firstName, string patronymic, double fixedRate)
            : base (id, name, firstName, patronymic)
        {
            _fixedRate = fixedRate;
        }

        public override double AverageMonthlyWage()
        {
            return _fixedRate;
        }

        public override EmployeeSalaryType SalaryType { get {return EmployeeSalaryType.Salary; } }

    }
}