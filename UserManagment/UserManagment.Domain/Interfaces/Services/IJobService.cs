using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserManagment.Domain.Models;

namespace UserManagment.Domain.Interfaces.Services
{
    public interface IJobService
    {
        IEnumerable<Job> GetJobsByUserName(string userName);

        void Insert(Job job);

        void Delete(Job job);

        void Update(Job job);

        IEnumerable<Job> Filter(Expression<Func<Job, bool>> filter);

        IEnumerable<Job> GetAll();

        IEnumerable<Job> GetJobsByUserId(int userId);

        Job Get(int id);

        bool IsExist(Expression<Func<Job, bool>> filter);
    }
}
