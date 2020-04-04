using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork<T> where T : class
    {
        internal VendorAPIWebEntities context;
        internal DbSet<T> dbset;

        public UnitOfWork(VendorAPIWebEntities context)
        {
            this.context = context;
            this.dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> Get()
        {
            IQueryable<T> query = dbset;
            return query.ToList();
        }

        public virtual T GetById(object id)
        {
            return dbset.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbset.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbset.Attach(entityToDelete);
            }
            dbset.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            dbset.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual IEnumerable<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            return context.Database.SqlQuery<T>(sql, parameters);
        }

        public virtual IEnumerable<T> SQLQueryWithoutParameters<T>(string sql)
        {
            return context.Database.SqlQuery<T>(sql);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return context.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
