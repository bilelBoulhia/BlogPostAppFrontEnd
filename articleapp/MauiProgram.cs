

using articleapp.auth;
using articleapp.Interfaces;
using articleapp.Pages;
using articleapp.Repo;
using articleapp.ViewModels;
using CloudinaryDotNet;
using CommunityToolkit.Maui;
using dotenv.net;
using Maui.FreakyControls.Extensions;

using Microsoft.Extensions.Logging;
using Serilog;
using Sharpnado.MaterialFrame;

#pragma warning disable CA1416 // Validate platform compatibility
namespace articleapp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            //DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            //Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            //cloudinary.Api.Secure = true;


            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSharpnadoMaterialFrame(loggerEnable: false)
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Jersey15-Regular.ttf", "Jersey15");
                    fonts.AddFont("fa-brands-400.ttf", "faBrands");
                    fonts.AddFont("fa-solid-900.ttf", "faSolid");
                });




            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<FinsishSignUpViewModel>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<SignupViewModel>();
            builder.Services.AddScoped<IUser, UserRepo>();
            builder.Services.AddScoped<IComments, CommentRepo>();
            builder.Services.AddScoped<IArticles, ArticleRepo>();
            builder.Services.AddSingleton<AuthContext>();
            builder.Services.AddTransient<CommentRepo>();
            builder.Services.AddTransient<ArticleRepo>();
            builder.Services.AddTransient<UserRepo>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<SignupPage>();
            builder.Services.AddTransient<FinishSignUpPage>();
            builder.Services.AddTransient<MainArticlePage>();
            builder.Services.AddTransient<LoginPage>();

        

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.InitializeFreakyControls();

            return builder.Build();
        }
    }


}
