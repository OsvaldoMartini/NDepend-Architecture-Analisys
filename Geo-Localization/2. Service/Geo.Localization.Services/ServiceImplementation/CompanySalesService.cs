using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Geo.Localization.Data;
using Geo.Localization.Data.IRepository;
using Geo.Localization.Data.Repository;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.ServiceContrat;

namespace Geo.Localization.Services.ServiceImplementation
{
    public class CompanySalesService : ICompanySalesService, IDisposable
    {
        #region Primitive Properties

        private readonly ICompanySalesRepository _companySalesRepository;

        #endregion

        #region Constructors

        public CompanySalesService()
        {
            //Mapper DTOs -> BOs  and BOs -> DTOS
            Mapper.CreateMap<CompanySalesDto, CompanySalesEntity>();
            Mapper.CreateMap<CompanySalesEntity, CompanySalesDto>();

            _companySalesRepository = new CompanySalesRepository();
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }

        public IList<CompanySalesDto> GetCompanySaleByType(string typeCompany)
        {
            var companiesSales = _companySalesRepository.GetCorpSalesByType(typeCompany);

            return Mapper.Map<IList<CompanySalesEntity>, IList<CompanySalesDto>>(companiesSales.ToList());
            
            //return Mapper.Map<CompanySalesEntity, CompanySalesDto > (companiesSales);
        }
      
    }
}