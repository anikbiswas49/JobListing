namespace JobListing.Models
{
    public class JobTag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }

    }
}
