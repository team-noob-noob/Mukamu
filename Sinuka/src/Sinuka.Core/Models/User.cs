namespace Sinuka.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
    }
}
