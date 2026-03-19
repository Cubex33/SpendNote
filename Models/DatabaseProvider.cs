using SQLite;

namespace SpendNote.Models
{
    public static class DatabaseProvider
    {
        public static void Init(string connectionFetch)
        {
            connection = new SQLiteConnection(connectionFetch);
        }

        public static SQLiteConnection? connection { get; set; }
    }
}
