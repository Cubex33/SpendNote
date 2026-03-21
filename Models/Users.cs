namespace SpendNote.Models
{
    public class Users
    {
        public string Id { get; set; }
        public string Username { get; set; } = Guid.NewGuid().ToString();
        public string Password { get; set; } = Guid.NewGuid().ToString();
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
    }
}
