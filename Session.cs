using System.Threading.Tasks;

namespace SpendNote
{
    public static class Session
    {
        public static string SessionId { get; set; } = null!;
        public static string SessionName { get; set; } = null!;
        public static int Remains { get; set; }
    }
}
