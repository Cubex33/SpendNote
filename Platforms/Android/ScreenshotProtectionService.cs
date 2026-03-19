using Android.Views;
using SpendNote.Interfaces;
namespace SpendNote.Platforms.Android
{
    public class ScreenshotProtectionService : IScreenshotProtectionService
    {
        public void Enable()
        {
            var activity = Platform.CurrentActivity;
            activity?.Window?.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);
        }

        public void Disable()
        {
            var activity = Platform.CurrentActivity;
            activity?.Window?.ClearFlags(WindowManagerFlags.Secure);
        }
    }
}
