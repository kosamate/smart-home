using Microsoft.Extensions.Logging;
using SonsOfUncleBob.ViewModels;
using Microcharts.Maui;

namespace SonsOfUncleBob
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

            builder.UseMicrocharts();

            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();


#endif
            var a = Program_main.Main();
            return builder.Build();
        }
    }
}
