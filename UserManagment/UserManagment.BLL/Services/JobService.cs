using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserManagment.Domain.Interfaces.Repositories;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Interfaces.UnitOfWork;
using UserManagment.Domain.Models;

namespace UserManagment.BLL.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jobRepository = unitOfWork.JobRepository;
            _userRepository = unitOfWork.UserRepository;
        }

        public void Delete(Job job)
        {
            if (!IsExist(x => x.Id == job.Id))
            {
                throw new ArgumentException("This job does not exsist");
            }

            _jobRepository.Delete(job);
            _unitOfWork.Commit();
        }

        public IEnumerable<Job> Filter(Expression<Func<Job, bool>> filter)
        {
            return _jobRepository.Filter(filter);
        }

        public Job Get(int id)
        {
            return _jobRepository.Get(id);
        }

        public IEnumerable<Job> GetAll()
        {
            return _jobRepository.GetAll();
        }

        public IEnumerable<Job> GetJobsByUserId(int userId)
        {
            return _jobRepository.GetJobsByUserId(userId);
        }

        public IEnumerable<Job> GetJobsByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            return _jobRepository.GetJobsByUserName(userName);
        }

        public void Insert(Job job)
        {
            try
            {
                CheckJobFields(job);
            }
            catch (ArgumentException e)
            {
                throw e;
            }

            _jobRepository.Insert(job);
            _unitOfWork.Commit();
        }

        public bool IsExist(Expression<Func<Job, bool>> filter)
        {
            return _jobRepository.IsExist(filter);
        }

        public void Update(Job job)
        {
            try
            {
                CheckJobFields(job);
            }
            catch (ArgumentException e)
            {
                throw e;
            }

            _jobRepository.Update(job);
            _unitOfWork.Commit();
        }

        private void CheckJobFields(Job job)
        {
            if (job == null)
            {
                throw new ArgumentException("Job is empty");
            }
            if (string.IsNullOrEmpty(job.Name))
            {
                throw new ArgumentException("Job name is empty");
            }
            if (string.IsNullOrEmpty(job.Status))
            {
                throw new ArgumentException("Job status is empty");
            }
            if (string.IsNullOrEmpty(job.Description))
            {
                throw new ArgumentException("Job description is empty");
            }
            if (!_userRepository.IsExist(x => x.Id == job.UserId))
            {
                throw new ArgumentException("user asign to this work not exsist");
            }
        }
    }
}
