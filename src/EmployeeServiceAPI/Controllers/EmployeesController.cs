using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Extensions;
using EmployeeServiceAPI.ModelServiceAPI;
using EmployeeServiceAPI.ModelServiceAPI.Repositories;

namespace EmployeeServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ILogger _logger;

        public EmployeesController(IUnitOfWorkFactory unitOfWorkFactory, ILogger<EmployeesController> logger)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _logger = logger;
        }

        // GET api/employees
        [HttpGet]
        public IEnumerable<EmployeeDTO> Get()
        {
            List<EmployeeDTO> employees = _unitOfWorkFactory.Create()
                                        .employeeRepository.GetEmployees()
                                        .OrderByDescending(e => e.ToEmployee().AverageMonthlyWage())
                                        // .ThenBy(e => e.name)
                                        .ToList();

            if (employees.Count == 0)
            {
                return new List<EmployeeDTO>();
            }
            
            return employees;
        }

        // GET api/employees/name-first-patr
        [HttpGet("{name}-{firstName}-{patronymic}")]
        public EmployeeDTO Get(string name, string firstName, string patronymic)
        {
            _logger.LogInformation("Поиск сотрудника по ФИО: {0} {1} {2}", name, firstName, patronymic);
            return _unitOfWorkFactory.Create().employeeRepository.GetEmployeeByFIO(name, firstName, patronymic);
        }

        // POST - add
        [HttpPost]
        public EmployeeDTO Post([FromBody]EmployeeDTO employee)
        {
            EmployeeDTO newEmployee = null;

            using(IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                try
                {
                    newEmployee = unitOfWork.employeeRepository.AddEmployee(employee);
                    unitOfWork.Commit();
                }
                catch(Exception ex)
                {
                    unitOfWork.Rollback();
                    _logger.LogError(ex, "Ошибка добавления сотрудника.");
                }
            }
            return newEmployee;
        }

        // PUT - update
        [HttpPut]
        public void Put([FromBody]Employee employee)
        {
        }

        // DELETE api/employees/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
