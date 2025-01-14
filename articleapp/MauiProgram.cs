using Maui.FreakyControls.Extensions;
using Microsoft.Extensions.Logging;
using Sharpnado.MaterialFrame;

namespace articleapp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSharpnadoMaterialFrame(loggerEnable: false)
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Jersey15-Regular.ttf", "Jersey15");
                    fonts.AddFont("Dhurjati-Regular.ttf", "Dhurjati");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.InitializeFreakyControls();

            return builder.Build();
        }
    }
}
