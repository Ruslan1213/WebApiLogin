using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UserManagment.DAL.Contexts;
using UserManagment.Domain.Interfaces.Repositories;

namespace UserManagment.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        private readonly Lazy<UserStoryContext> _context;

        public Repository(UserStoryContext context)
        {
            _dbSet = context.Set<T>();
            _context = new Lazy<UserStoryContext>(() => { return context; });
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public virtual T Get(int id)
        {
            return _dbSet.Find(id); ;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public bool IsExist(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public void Update(T entity)
        {
            _context.Value.Entry(entity).State = EntityState.Modified;
        }
    }
}
