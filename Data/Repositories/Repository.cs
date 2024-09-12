using EcomMVC.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomMVC.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        } 

        public void Delete (object Id)
        {
            T entity = _dbContext.Set<T>().Find(Id);
            if (entity!=null){
                _dbContext.Set<T>().Remove(entity);
            }
        }
        public T Find (object Id)
        {

            return _dbContext.Set<T>().Find(Id)?? throw new InvalidOperationException("Entity not found");
        } 

        public IEnumerable<T> GetAll(){
            return _dbContext.Set<T>().ToList();
        }

        public void Remove (T entity){
            _dbContext.Set<T>().Remove(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

    }
}