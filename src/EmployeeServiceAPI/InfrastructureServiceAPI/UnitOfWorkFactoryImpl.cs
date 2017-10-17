using Microsoft.Extensions.Options;
using EmployeeServiceAPI.ModelServiceAPI;
using EmployeeServiceAPI.ModelServiceAPI.Repositories;
using EmployeeServiceAPI.ModelServiceAPI.Configuration;

namespace EmployeeServiceAPI.InfrastructureServiceAPI
{
    internal class UnitOfWorkFactoryImpl : IUnitOfWorkFactory
    {
        private string _connectionString;
        public UnitOfWorkFactoryImpl(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.DbConnection;
            //_connectionString = "server=localhost;database=employees;integrated security=false;user id=sa;password=1";
        }
        public string ConnectionString { get {return _connectionString; } }
        public IUnitOfWork Create()
        {
            return new InfrastructureServiceAPI.Repositories.UnitOfWorkImpl(_connectionString);
        }
    }
}