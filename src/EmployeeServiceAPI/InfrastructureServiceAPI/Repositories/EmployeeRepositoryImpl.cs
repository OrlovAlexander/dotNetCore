
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using Model;
using Model.Enums;
using EmployeeServiceAPI.ModelServiceAPI.Repositories;

namespace EmployeeServiceAPI.InfrastructureServiceAPI.Repositories
{
    public class EmployeeRepositoryImpl : IEmployeeRepository
    {
        private UnitOfWorkImpl _unitOfWorkImpl;
        public EmployeeRepositoryImpl(UnitOfWorkImpl unitOfWorkImpl)
        {
            _unitOfWorkImpl = unitOfWorkImpl;
        }

        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee e INNER JOIN EmployeeSalary es ON e.id = es.EmployeeId", _unitOfWorkImpl.Connection))
            {
                cmd.Transaction = _unitOfWorkImpl.Transaction;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        employees
                            .Add(new EmployeeDTO
                                {
                                    id = Convert.ToInt32(reader["Id"]),
                                    name = reader["Name"].ToString(),
                                    firstName = reader["FirstName"].ToString(),
                                    patronymic = reader["Patronymic"].ToString(),
                                    salaryType = EmployeeSalaryType.Salary,
                                    pay = Convert.ToDouble(reader["Salary"])
                                }
                            );
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee e INNER JOIN EmployeeHourlyPay hp ON e.id = hp.EmployeeId", _unitOfWorkImpl.Connection))
            {
                cmd.Transaction = _unitOfWorkImpl.Transaction;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        employees
                            .Add(new EmployeeDTO
                                {
                                    id = Convert.ToInt32(reader["Id"]),
                                    name = reader["Name"].ToString(),
                                    firstName = reader["FirstName"].ToString(),
                                    patronymic = reader["Patronymic"].ToString(),
                                    salaryType = EmployeeSalaryType.HourlyPay,
                                    pay = Convert.ToDouble(reader["HourlyPay"])
                                }
                            );
                    }
                }
            }
            return employees;
        }

        public EmployeeDTO GetEmployeeByFIO(string name, string firstName, string patronymic)
        {
            EmployeeDTO employee = null;

            int id = 0;

            string commandText = string.Format("SELECT e.Id FROM Employee e WHERE e.Name = '{0}' AND e.FirstName = '{1}' AND e.Patronymic = '{2}'", name, firstName, patronymic);
            using (SqlCommand cmd = new SqlCommand(commandText, _unitOfWorkImpl.Connection))
            {
                cmd.Transaction = _unitOfWorkImpl.Transaction;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader["Id"]);
                    }
                }
            }

            double? salary = null;
            commandText = string.Format("SELECT * FROM EmployeeSalary es WHERE es.EmployeeId = {0}", id);
            using (SqlCommand cmd = new SqlCommand(commandText, _unitOfWorkImpl.Connection))
            {
                cmd.Transaction = _unitOfWorkImpl.Transaction;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        salary = Convert.ToDouble(reader["Salary"]);
                    }
                }
            }

            double? hourlyPay = null;
            commandText = string.Format("SELECT * FROM EmployeeHourlyPay hp WHERE hp.EmployeeId = {0}", id);
            using (SqlCommand cmd = new SqlCommand(commandText, _unitOfWorkImpl.Connection))
            {
                cmd.Transaction = _unitOfWorkImpl.Transaction;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        hourlyPay = Convert.ToDouble(reader["HourlyPay"]);
                    }
                }
            }

            if (salary.HasValue)
            {
                employee = new EmployeeDTO
                    {
                        id = id, 
                        name = name, 
                        firstName = firstName, 
                        patronymic = patronymic,
                        salaryType = EmployeeSalaryType.Salary, 
                        pay = salary.Value
                    };
            }
            else if (hourlyPay.HasValue)
            {
                employee = new EmployeeDTO
                    {
                        id = id, 
                        name = name, 
                        firstName = firstName, 
                        patronymic = patronymic,
                        salaryType = EmployeeSalaryType.HourlyPay, 
                        pay = hourlyPay.Value
                    };
            }
            else
            {
                employee = null;
            }

            return employee;
        }

        public EmployeeDTO AddEmployee(EmployeeDTO employee)
        {
            EmployeeDTO newEmployee = GetEmployeeByFIO(employee.name, employee.firstName, employee.patronymic);

            if (newEmployee != null)
            {
                return newEmployee;
            }

            string insertCommand = string.Format(@"INSERT INTO [dbo].[Employee]
                ([Name]
                ,[FirstName]
                ,[Patronymic])
               VALUES
                ('{0}','{1}','{2}');
               SELECT @Id = SCOPE_IDENTITY();"
             , employee.name
             , employee.firstName
             ,employee.patronymic);

            int? id = null;
            using (SqlCommand cmd = new SqlCommand(insertCommand, _unitOfWorkImpl.Connection))
            {
                cmd.Transaction = _unitOfWorkImpl.Transaction;
                SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.Int);
                idParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(idParameter);

                cmd.ExecuteNonQuery();

                id = Convert.ToInt32(idParameter.Value);
            }

            if (!id.HasValue || id == 0)
            {
                return null;
            }

            if (employee.salaryType == EmployeeSalaryType.Salary)
            {
                insertCommand = string.Format(@"INSERT INTO [dbo].[EmployeeSalary]
                    ([EmployeeId], [Salary])
                VALUES
                    ({0}, {1})"
                ,id
                ,employee.pay.ToString(CultureInfo.GetCultureInfo("en-US")));

                using (SqlCommand cmd = new SqlCommand(insertCommand, _unitOfWorkImpl.Connection))
                {
                    cmd.Transaction = _unitOfWorkImpl.Transaction;
                    cmd.ExecuteNonQuery();
                }
            }

            if (employee.salaryType == EmployeeSalaryType.HourlyPay)
            {
                insertCommand = string.Format(@"INSERT INTO [dbo].[EmployeeHourlyPay]
                    ([EmployeeId], [HourlyPay])
                VALUES
                    ({0}, {1})"
                ,id
                ,employee.pay.ToString(CultureInfo.GetCultureInfo("en-US")));

                using (SqlCommand cmd = new SqlCommand(insertCommand, _unitOfWorkImpl.Connection))
                {
                    cmd.Transaction = _unitOfWorkImpl.Transaction;
                    cmd.ExecuteNonQuery();
                }
            }

            newEmployee = new EmployeeDTO 
                { 
                    id = id.Value, 
                    name = employee.name, 
                    firstName = employee.firstName, 
                    patronymic = employee.patronymic, 
                    salaryType = employee.salaryType, 
                    pay = employee.pay
                };

            return newEmployee;
        }
    }
}