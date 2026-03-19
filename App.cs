using SpendNote.Interfaces;
using SpendNote.Pages;

namespace SpendNote
{
    public class App : Application
    {
        public App(IScreenshotProtectionService protectionService)
        {
            MainPage = new NavigationPage(new MainPage(protectionService));
        }
    }
}