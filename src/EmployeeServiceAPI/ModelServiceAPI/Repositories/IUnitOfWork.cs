
using System;

namespace EmployeeServiceAPI.ModelServiceAPI.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository employeeRepository { get; }

        void Commit();
        void Rollback();
    }
}
