using Microsoft.Extensions.Logging;
using Produit.Infrastructure;
using Produit.Services;
using Produit.View;
using Produit.View_Model;

namespace Produit;

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
        // Ajout des services
        builder.Services.AddScoped<IProduitService, ProduitService>();

        builder.Services.AddHttpClient<IProduitService, ProduitService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7179/api/");
        });

        // Ajout des ViewModels
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<AddProductViewModel>();
        // Ajout de la page
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AddProducts>();

        return builder.Build();
    }
}
