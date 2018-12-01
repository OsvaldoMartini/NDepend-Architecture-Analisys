using System.Collections.Generic;
using Geo.Localization.Data.Utils;

namespace Geo.Localization.Data.IRepository
{
    public interface ICompanySaleRepository : IGenericRepository<CompanySaleEntity>
    {
        List<CompanySaleEntity> GetCorpSaleByType(string typeCompany); //Return Company Sales By Type

    }
}