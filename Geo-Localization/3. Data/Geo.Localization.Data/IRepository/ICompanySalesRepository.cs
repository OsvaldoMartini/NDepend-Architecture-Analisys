using System.Collections.Generic;
using Geo.Localization.Data.Utils;

namespace Geo.Localization.Data.IRepository
{
    public interface ICompanySalesRepository : IGenericRepository<CompanySalesEntity>
    {
        IEnumerable<CompanySalesEntity> GetCorpSalesByType(string typeCompany); //Return Company Sales By Type

    }
}