using Microsoft.Extensions.Logging;
using SonsOfUncleBob.Http;
using SonsOfUncleBob.Http.DTO;
using System.Diagnostics;

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
#if DEBUG
            builder.Logging.AddDebug();
#endif
            Program.Main();

            return builder.Build();
        }
    }
}