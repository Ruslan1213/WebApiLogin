using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserManagment.Domain.Models;

namespace UserManagment.Domain.Interfaces.Services
{
    public interface IRoleService
    {
        void Insert(Role role);

        void Delete(Role role);

        void Update(Role role);

        IEnumerable<Role> Filter(Expression<Func<Role, bool>> filter);

        IEnumerable<Role> GetAll();

        Role Get(int id);

        bool IsExist(Expression<Func<Role, bool>> filter);

        Role GetByName(string name);
    }
}
