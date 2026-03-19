using Microsoft.Extensions.Logging;
using SpendNote.Interfaces;
using SpendNote.Models;

#if ANDROID
using SpendNote.Platforms.Android;
#endif

namespace SpendNote
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if ANDROID
            builder.Services.AddSingleton<IScreenshotProtectionService, ScreenshotProtectionService>();
#else
            builder.Services.AddSingleton<IScreenshotProtectionService, DefaultScreenshotProtectionService>();
#endif
            builder.Services.AddSingleton<App>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            DatabaseProvider.Init(Path.Combine(FileSystem.AppDataDirectory, "spendnote.db"));
            DatabaseProvider.connection?.CreateTable<Users>();
            return builder.Build();
        }
    }
}