using Autofac.Features.Indexed;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UserManagment.Domain.Enums;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Models;
using UserManagment.Extensions;
using UserManagment.Models;

namespace UserManagment.Controllers
{
    [Authorize]
    public class TaskController : ApiController
    {
        private readonly IIndex<Roles, IRigtsResolver> _states;

        private readonly IJobService _jobService;

        private readonly IMapper _mapper;

        private IRigtsResolver _rigtsResolver;

        public TaskController(
            IJobService jobService,
            IMapper mapper,
            IIndex<Roles, IRigtsResolver> states)
        {
            _jobService = jobService;
            _mapper = mapper;
            _states = states;
        }

        [HttpGet]
        [Route("api/job")]
        public IEnumerable<JobViewModel> GetJobs()
        {
            var role = this.GetRole();

            if (role == null)
            {
                return null;
            }

            Roles enumRole = (Roles)Enum.Parse(typeof(Roles), role);
            _rigtsResolver = _states[enumRole];
            var jobs = _rigtsResolver.GetJobsByRole(User.Identity.Name).ToList();

            return jobs.Select(x => _mapper.Map<JobViewModel>(x));
        }

        [HttpGet]
        [Route("api/job/{id}")]
        public IHttpActionResult GetJob(int id)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return BadRequest("Job with this id was not found");
            }

            return Ok(_mapper.Map<JobViewModel>(job));
        }

        [HttpPost]
        [Route("api/job")]
        public IHttpActionResult PostJob([FromBody]CreateJobViewModel createJobModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.GetModelErrors());
            }

            try
            {
                _jobService.Insert(_mapper.Map<Job>(createJobModel));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Route("api/job/{id}")]
        public IHttpActionResult PutJob(int id, [FromBody]UpdateJobModel updateJobModel)
        {
            if (!ModelState.IsValid || updateJobModel == null)
            {
                return BadRequest(this.GetModelErrors());
            }

            Job job = _jobService.Get(id);

            if (job == null)
            {
                return BadRequest("Job with this id was not found");
            }

            try
            {
                job = _mapper.Map(updateJobModel, job);
                _jobService.Update(job);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("api/job/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return BadRequest("Job with this id was not found");
            }

            _jobService.Delete(job);

            return Ok();
        }
    }
}
