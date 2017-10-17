using EmployeeServiceAPI.ModelServiceAPI.Repositories;

namespace EmployeeServiceAPI.ModelServiceAPI
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
        string ConnectionString { get; }
    }
}
