namespace JobListing.Utils
{
    public class HashPasswordUtil
    {
        public string HashPassword(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }

        public bool VerifyPassword(string hashPassword,string textPassword)
        {
            return BCrypt.Net.BCrypt.Verify(textPassword,hashPassword );
        }
    }
}
