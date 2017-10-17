using Model.Enums;

namespace Model
{
    public class EmployeeDTO
    {
        public int id {get; set;}
        public string name {get; set;}
        public string firstName {get;set;}
        public string patronymic {get;set;}
        public EmployeeSalaryType salaryType {get;set;}
        public double pay {get;set;}
        public double monthPayAvg { get; set; }
    }
}
