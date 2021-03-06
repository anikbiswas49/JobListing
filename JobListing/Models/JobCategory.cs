using System.Text.Json.Serialization;

namespace JobListing.Models
{
    public class JobCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        public ICollection<JobInfo> Jobs { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }

    }
}
