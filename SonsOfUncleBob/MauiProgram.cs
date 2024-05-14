﻿using Microsoft.Extensions.Logging;
using SonsOfUncleBob.ViewModels;
using Microcharts.Maui;
using SonsOfUncleBob.Models;
using SonsOfUncleBob.Database;

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

            builder.AddModels();
            builder.AddViewModels();
            builder.AddView();


#if DEBUG
            builder.Logging.AddDebug();


#endif
            var a = Program_main.Main();
            return builder.Build();
        }

        private static MauiAppBuilder AddModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<BarcelonaHomeModel>();
            builder.Services.AddSingleton<HistoryModel>();
            return builder;
        }

        private static MauiAppBuilder AddViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<HistoryViewModel>();
            builder.Services.AddSingleton<InformationViewModel>();
            return builder;
        }

        private static MauiAppBuilder AddView(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MainPage>();
            return builder;
        }
    }
}
