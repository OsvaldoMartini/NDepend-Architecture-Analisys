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
using Geo.Localization.Services.Utils;

namespace Geo.Localization.Services.ServiceImplementation
{
    public class CompanySalesService : ICompanySalesService, IDisposable
    {
        #region Primitive Properties

        private readonly ICompanySaleRepository _companySalesRepository;

        #endregion

        #region Constructors

        public CompanySalesService()
        {
            //Mapper DTOs -> BOs  and BOs -> DTOS
            //Mapper.CreateMap<CompanySaleDto, CompanySaleEntity>();
            //Mapper.CreateMap<CompanySaleEntity, CompanySaleDto>();
            ModelMapper.Configure();
            _companySalesRepository = new CompanySaleRepository();
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

        public List<CompanySaleDto> GetCompanySaleByType(string typeCompany)
        {
            List<CompanySaleEntity> companiesSales = _companySalesRepository.GetCorpSaleByType(typeCompany);

            return Mapper.Map<List<CompanySaleEntity>, List<CompanySaleDto>>(companiesSales);
            
            //return Mapper.Map<CompanySaleEntity, CompanySaleDto > (companiesSales);
        }
      
    }
}