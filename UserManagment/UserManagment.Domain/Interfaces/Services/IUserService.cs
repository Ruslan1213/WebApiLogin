using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserManagment.Domain.Models;

namespace UserManagment.Domain.Interfaces.Services
{
    public interface IUserService
    {
        void Insert(User user);

        void Delete(User user);

        void Update(User user);

        IEnumerable<User> Filter(Expression<Func<User, bool>> filter);

        IEnumerable<User> GetAll();

        User Get(int id);

        bool IsExist(Expression<Func<User, bool>> filter);

        User GetByName(string name);

        User GetByCredential(string name, string password, int roleId);

    }
}
