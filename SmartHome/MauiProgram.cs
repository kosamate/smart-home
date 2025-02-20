﻿using Microsoft.Extensions.Logging;
using SmartHome.ViewModels;
using Microcharts.Maui;
using SmartHome.Models;
using SmartHome.Database;
using Syncfusion.Maui.Core.Hosting;
using SmartHome.Http;

namespace SmartHome
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
            builder.ConfigureSyncfusionCore();
            builder.AddModels();
            builder.AddViewModels();
            builder.AddView();
            builder.Services.AddDbContext<HistoryDbContext>();


#if DEBUG
            builder.Logging.AddDebug();


#endif

            DataProvider dataProvider = DataProvider.Instance;

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
