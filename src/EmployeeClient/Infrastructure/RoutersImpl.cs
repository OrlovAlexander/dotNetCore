using EmployeeClient.Model;

namespace EmployeeClient.Infrastructure
{
    public class RoutersImpl : IRouters
    {
        public string baseAddress => "http://localhost:5000/";
        public string employeeApi => "api/Employees";
    }
}