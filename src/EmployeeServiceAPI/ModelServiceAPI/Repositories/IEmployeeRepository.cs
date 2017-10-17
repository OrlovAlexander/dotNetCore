using System.Collections.Generic;
using Model;

namespace EmployeeServiceAPI.ModelServiceAPI.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeDTO> GetEmployees();
        EmployeeDTO GetEmployeeByFIO(string name, string firstName, string patronymic);
        EmployeeDTO AddEmployee(EmployeeDTO employee);
    }
}