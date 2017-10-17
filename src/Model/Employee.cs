using System;
using Model.Enums;

namespace Model
{
    public abstract class Employee : Entity
    {
        public string Name { get; private set; }
        public string FirstName { get; private set; }
        public string Patronymic { get; private set; }

        public Employee(int id, string name, string firstName, string patronymic)
            : base (id)
        {
            this.Name = name;
            this.FirstName = firstName;
            this.Patronymic = patronymic;
        }

        public abstract double AverageMonthlyWage();

        public abstract EmployeeSalaryType SalaryType {get;}
    }
}