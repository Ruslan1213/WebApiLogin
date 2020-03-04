using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserManagment.DAL.Extensions;
using UserManagment.Domain.Interfaces.Repositories;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Interfaces.UnitOfWork;
using UserManagment.Domain.Models;

namespace UserManagment.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IRoleRepository _roleRepository;

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.UserRepository;
            _roleRepository = _unitOfWork.RoleRepository;
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public IEnumerable<User> Filter(Expression<Func<User, bool>> filter)
        {
            return _userRepository.Filter(filter);
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByCredential(string name, string password, int roleId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _userRepository.GetByCredential(name, password.GetHashString());

            if (user.RoleId != roleId)
            {
                return null;
            }

            return user;
        }

        public User GetByName(string name)
        {
            return _userRepository.GetByName(name);
        }

        public void Insert(User user)
        {
            try
            {
                CheckInsertableUser(user);
            }
            catch (ArgumentException e)
            {
                throw e;
            }

            user.Password = user.Password.GetHashString();
            _userRepository.Insert(user);
            _unitOfWork.Commit();
        }

        public bool IsExist(Expression<Func<User, bool>> filter)
        {
            return _userRepository.IsExist(filter);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        private void CheckInsertableUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("User is null");
            }
            if (IsExist(x => x.Name == user.Name))
            {
                throw new ArgumentException("User with this name already exist");
            }
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException("Not all fields are filled");
            }
            if (!_roleRepository.IsExist(x => x.Id == user.RoleId))
            {
                throw new ArgumentException("User role does not exist");
            }
        }
    }
}
