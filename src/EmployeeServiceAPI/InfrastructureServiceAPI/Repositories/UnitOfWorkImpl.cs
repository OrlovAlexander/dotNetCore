using System;
using System.Data;
using System.Data.SqlClient;
using EmployeeServiceAPI.ModelServiceAPI.Repositories;
using static System.Console;

namespace EmployeeServiceAPI.InfrastructureServiceAPI.Repositories
{
    public class UnitOfWorkImpl : IUnitOfWork
    {
        private SqlConnection _connection;
        public SqlConnection Connection { get { return _connection; } }
        private SqlTransaction _transaction;
        public SqlTransaction Transaction { get { return _transaction; } }

        public UnitOfWorkImpl(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            //_connection.InfoMessage += (sender, e) => { WriteLine($"warning or info {e.Message}"); };
            //_connection.StateChange += (sender, e) => { WriteLine($"current state: {e.CurrentState}, before: {e.OriginalState}"); };
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            _employeeRepository = new EmployeeRepositoryImpl(this);
        }
        private IEmployeeRepository _employeeRepository;
        public IEmployeeRepository employeeRepository { get { return _employeeRepository; } }
        public void Commit()
        {
            if (_connection == null)
                return;
            switch (_connection.State)
            {
                case ConnectionState.Closed : break;
                case ConnectionState.Open : _transaction.Commit(); break;
                case ConnectionState.Connecting : break;
                case ConnectionState.Executing : break;
                case ConnectionState.Fetching : break;
                case ConnectionState.Broken : break;
                default: break;
            }
        }
        public void Rollback()
        {
            if (_connection == null)
                return;
            switch (_connection.State)
            {
                case ConnectionState.Closed : break;
                case ConnectionState.Open : _transaction.Rollback(); break;
                case ConnectionState.Connecting : break;
                case ConnectionState.Executing : break;
                case ConnectionState.Fetching : break;
                case ConnectionState.Broken : break;
                default: break;
            }
        }
        public void Dispose()
        {
            _transaction = null;
            _connection = null;
        }
    }
}