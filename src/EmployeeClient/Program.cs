using Model;
using Model.Enums;
using Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using static System.Console;

namespace EmployeeClient
{
    class Program
    {
        private static string _baseAddress = "http://localhost:5000/";
        private static string _employeeApi = "api/Employees";

        static void Main(string[] args)
        {
            WriteLine("Client app, wait for service");
            try
            {
                ReadLine();
                //ReadAllEmployees().Wait();
                AddEmployee(new EmployeeDTO { id = 0,
                        name = "Павлов", firstName = "Николай", patronymic = "Игоревич",
                        salaryType = EmployeeSalaryType.HourlyPay,
                        pay = 320.0
                    }).Wait();
                AddEmployee(new EmployeeDTO { id = 0,
                        name = "Крылов", firstName = "Алексей", patronymic = "Михайлович",
                        salaryType = EmployeeSalaryType.Salary,
                        pay = 80000.0
                    }).Wait();
                AddEmployee(new EmployeeDTO { id = 0,
                        name = "Антонов", firstName = "Александр", patronymic = "Андреевич",
                        salaryType = EmployeeSalaryType.Salary,
                        pay = 40000.0
                    }).Wait();
                AddEmployee(new EmployeeDTO { id = 0,
                        name = "Бабин", firstName = "Антон", patronymic = "Викторович",
                        salaryType = EmployeeSalaryType.HourlyPay,
                        pay = 300.5
                    }).Wait();
                AddEmployee(new EmployeeDTO { id = 0,
                        name = "Кашин", firstName = "Влидимир", patronymic = "Александрович",
                        salaryType = EmployeeSalaryType.HourlyPay,
                        pay = 320.0
                    }).Wait();
                ReadEmployeeByFIO("Антонов", "Александр", "Андреевич").Wait();
                ReadEmployeeByFIO("Проснин", "Виктор", "Юрьевич").Wait();
                ReadAllEmployees().Wait();
                GetSummPayAllEmployes().Wait();


            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            ReadLine();
        }

        private static async Task ReadAllEmployees()
        {
            WriteLine(nameof(ReadAllEmployees));
            var client = new EmployeeAPIClient(_baseAddress);
            IEnumerable<EmployeeDTO> employees = await client.GetAllAsync(_employeeApi);
            if (employees.Count() > 0)
            {
                IEnumerable<EmployeeDTO> employeesSorted = employees
                    .OrderByDescending(e => e.ToEmployee().AverageMonthlyWage())
                    .ThenBy(e => e.name)
                    .ToList();

                foreach(EmployeeDTO employee in employees)
                {
                    WriteLine("{0} {1} {2} {3} {4}", employee.id, employee.name, employee.firstName, employee.patronymic, employee.ToEmployee().AverageMonthlyWage());
                }

                double avgPay = employees.Average(e => e.ToEmployee().AverageMonthlyWage());
                List<EmployeeDTO> employeesWithPayOverAvgPay = employees
                                                            .Where(e => e.pay > avgPay)
                                                            .ToList();
                
                if (employeesWithPayOverAvgPay.Count() > 0)
                {
                    FileStream stream = File.OpenWrite("EmployeesWithPayOverAvgPay.xml");
                    using (TextWriter writer = new StreamWriter(stream))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(EmployeeDTO[]));
                        serializer.Serialize(writer, employeesWithPayOverAvgPay.ToArray());
                    }
                }
                
            }
            WriteLine("Выполнено.");
            WriteLine();
        }

        private static async Task AddEmployee(EmployeeDTO employee)
        {
            WriteLine(nameof(AddEmployee));
            var client = new EmployeeAPIClient(_baseAddress);
            EmployeeDTO addedEmployee = await client.PostAsync(_employeeApi, employee);
            WriteLine("{0} {1} {2} {3} {4}", addedEmployee.id, addedEmployee.name, addedEmployee.firstName, addedEmployee.patronymic, addedEmployee.pay);
            WriteLine("Выполнено.");
            WriteLine();
        }

        private static async Task ReadEmployeeByFIO(string name, string firstName, string patronymic)
        {
            WriteLine(nameof(ReadEmployeeByFIO));
            var client = new EmployeeAPIClient(_baseAddress);
            string uri = string.Format("{0}/{1}-{2}-{3}", _employeeApi, name, firstName, patronymic);
            EmployeeDTO employee = await client.GetAsync(uri);
            if (employee != null)
            {
                WriteLine("{0} {1} {2} {3} {4}", employee.id, employee.name, employee.firstName, employee.patronymic, employee.pay);
            }
            else
            {
                WriteLine("Сотрудник {0} {1} {2} не найден.", name, firstName, patronymic);
            }
            WriteLine("Выполнено.");
            WriteLine();
        }

        private static async Task GetSummPayAllEmployes()
        {
            WriteLine(nameof(GetSummPayAllEmployes));
            var client = new EmployeeAPIClient(_baseAddress);
            IEnumerable<EmployeeDTO> employees = await client.GetAllAsync(_employeeApi);
            if (employees.Count() > 0)
            {
                double summPay = employees.Sum(e => e.ToEmployee().AverageMonthlyWage());
                WriteLine("Суммарная зарплатаа всех сотрудников за месяц: {0}", summPay);
            }
            WriteLine("Выполнено.");
            WriteLine();
        }
    }
}
