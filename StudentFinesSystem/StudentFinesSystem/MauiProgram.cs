using Microsoft.Extensions.Logging;
using epj.RouteGenerator;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using StudentFinesSystem.Helpers;
using StudentFinesSystem.Helpers.Interface;
using CommunityToolkit.Maui;
using StudentFinesSystem.Pages;
using CommunityToolkit.Maui.Core;
using StudentFinesSystem.Service;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Models;
namespace StudentFinesSystem
{
    [AutoRoutes("Page")]
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("StudentFinesSystem.appsettings.json");

            var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

            builder.Configuration.AddConfiguration(config);

            builder.Services.AddTransient<ICustomPopupService, CustomPopupService>();
            builder.Services.AddSingleton<IAPIHelper, APIHelper>();
            builder.Services.AddSingleton<ILoginUser, LoginUser>();
            builder.Services.AddSingleton<App>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<StudentsPage>();
            builder.Services.AddSingleton<CreateAccountPage>();
            builder.Services.AddSingleton<CreateNamePage>();
            builder.Services.AddSingleton<FinesListPage>();
            builder.Services.AddSingleton<AccountPage>();

            builder.Services.AddSingleton<AddPopUpPage>();
            builder.Services.AddSingleton<UpdatePopUpPage>();
            builder.Services.AddSingleton<AddStudentPopUpPage>();
            builder.Services.AddSingleton<StudentViewPage>();
            builder.Services.AddSingleton<MyFinesPage>();

            builder.Services.AddSingleton<StudentShell>();
            builder.Services.AddSingleton<AdminShell>();

            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}
