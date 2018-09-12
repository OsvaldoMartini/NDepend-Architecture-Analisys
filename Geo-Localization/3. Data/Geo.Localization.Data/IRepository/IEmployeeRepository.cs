using Geo.Localization.Data.Utils;

namespace Geo.Localization.Data.IRepository
{
    public interface IEmployeeRepository : IGenericRepository<EmployeeEntity>
    {
        EmployeeEntity FindByUserName(EmployeeEntity _employee);
    }
}