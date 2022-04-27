namespace JobListing.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address{ get; set; }
        public string Logo { get; set; }

        public User User { get; set; }
        public ICollection<JobInfo> Jobs { get; set; }

    }
}
