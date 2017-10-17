
using Model;
using Model.Enums;

namespace Model.Extensions
{
    public static class EmployeeDTOConvert
    {
        public static Employee ToEmployee(this EmployeeDTO employeeDTO)
        {
            Employee employee = null;

            if (employeeDTO == null)
                return employee;

            if (employeeDTO.salaryType == EmployeeSalaryType.Salary)
            {
                employee = new EmployeeSalary(employeeDTO.id, employeeDTO.name, employeeDTO.firstName, employeeDTO.patronymic, employeeDTO.pay);
            }
            else if (employeeDTO.salaryType == EmployeeSalaryType.HourlyPay)
            {
                employee = new EmployeeHourlyPay(employeeDTO.id, employeeDTO.name, employeeDTO.firstName, employeeDTO.patronymic, employeeDTO.pay);
            }
            else
            {
                throw new System.Exception("Неизвестный тип оплаты труда.");
            }

            return employee;
        }
    }
}