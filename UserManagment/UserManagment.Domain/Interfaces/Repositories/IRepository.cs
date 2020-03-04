using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UserManagment.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);

        void Delete(T entity);

        void Update(T entity);

        IEnumerable<T> Filter(Expression<Func<T, bool>> filter);

        IEnumerable<T> GetAll();

        T Get(int id);

        bool IsExist(Expression<Func<T, bool>> filter);
    }
}
