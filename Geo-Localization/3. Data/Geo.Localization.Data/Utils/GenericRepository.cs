using System;
using System.Collections.Generic;
using System.Linq;

namespace Geo.Localization.Data.Utils
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseBO
    {
        #region private Methods

        private void Validate(T entity)
        {
            if (entity is IValidation)
                entity.Validation();
        }

        #endregion


        #region Public Method IGenericRepository

        public IQueryable<T> GetQuery()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Func<T, bool> where)
        {
            throw new NotImplementedException();
        }

        public void Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual string Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual string Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual string Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public virtual int Insert(T entity)
        {
            throw new NotImplementedException();
        }


        public virtual IEnumerable<T> GetAllByCompany(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T FindByID(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion


        public virtual IEnumerable<T> GetAll(GeoLocalizationEntity _geoLocalization)
        {
            throw new NotImplementedException();
        }
    }
}