using SQLite;

namespace SpendNote.Models
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        int Id { get; set; }
        public string Username { get; set; } = Guid.NewGuid().ToString();
        public string Password { get; set; } = Guid.NewGuid().ToString();
    }
}
