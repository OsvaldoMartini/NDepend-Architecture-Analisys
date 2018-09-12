using System;
using AutoMapper;
using Geo.Localization.Data;
using Geo.Localization.Data.IRepository;
using Geo.Localization.Data.Repository;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.ServiceContrat;

namespace Geo.Localization.Services.ServiceImplementation
{
    public class CompanyService : ICompanyService, IDisposable
    {
        #region Primitive Properties

        private readonly ICompanyRepository _companyRepository;

        #endregion

        #region Constructors

        public CompanyService()
        {
            //Mapper DTOs -> BOs  and BOs -> DTOS
            Mapper.CreateMap<CompanyDto, CompanyEntity>();
            Mapper.CreateMap<CompanyEntity, CompanyDto>();

            _companyRepository = new CompanyRepository();
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

        public CompanyDto FindByID(int id)
        {
            var company = _companyRepository.FindByID(id);

            return Mapper.Map<CompanyEntity, CompanyDto>(company);
        }
    }
}