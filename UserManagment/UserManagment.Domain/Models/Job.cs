﻿namespace UserManagment.Domain.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
