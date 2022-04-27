using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListing.Models
{
    public class JobInfo
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public int Vacancy { get; set; }
        public string EmploymentStatus { get; set; }
        public int Experience { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string JobLocation { get; set; }
        public JobCategory Category { get; set; }
        public Company JobCompany { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public DateTime ApplicationDeadline { get; set; }
    }
}
