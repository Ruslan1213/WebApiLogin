using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserManagment.Domain.Interfaces.Repositories;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Interfaces.UnitOfWork;
using UserManagment.Domain.Models;

namespace UserManagment.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = _unitOfWork.RoleRepository;
        }

        public void Delete(Role role)
        {
            _roleRepository.Delete(role);
        }

        public IEnumerable<Role> Filter(Expression<Func<Role, bool>> filter)
        {
            return _roleRepository.Filter(filter);
        }

        public Role Get(int id)
        {
            return _roleRepository.Get(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public Role GetByName(string name)
        {
            return _roleRepository.GetByName(name);
        }

        public void Insert(Role role)
        {
            _roleRepository.Insert(role);
        }

        public bool IsExist(Expression<Func<Role, bool>> filter)
        {
            return _roleRepository.IsExist(filter);
        }

        public void Update(Role role)
        {
            _roleRepository.Update(role);
        }
    }
}
