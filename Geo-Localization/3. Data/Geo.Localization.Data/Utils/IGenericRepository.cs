using System;
using System.Collections.Generic;
using System.Linq;

namespace Geo.Localization.Data.Utils
{
    public interface IGenericRepository<T> : IDisposable where T : BaseBO
    {
        IQueryable<T> GetQuery(); //Return Data from Any Querys Set  
        IEnumerable<T> GetAllByCompany(T entity); //Return all Entity data  
        IEnumerable<T> GetAll(); //Return all Entity data  
        
        IEnumerable<T> Find(Func<T, bool> where); ////Will return values often an expression
        T FindByID(T entity);
        T FindByID(int id); 
        void Attach(T entity); //Puts the Object in the Entity Framework cache
        string Delete(T entity);
        string Delete(int id);
        void Add(T entity);
        string Update(T entity);
        void SaveChanges();
        int Insert(T entity);

    }
}