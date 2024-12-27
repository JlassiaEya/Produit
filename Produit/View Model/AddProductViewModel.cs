using MvvmHelpers;
using Produit.Entities;
using Produit.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Produit.View_Model;

public class AddProductViewModel : BaseViewModel
{
    private readonly IProduitService _produitService;

   
    public string IdProd { get; set; }
    public string Libelle { get; set; }
    public string Qt { get; set; }
    public string Prix { get; set; }

    public ICommand AddProductCommand { get; }
    public AddProductViewModel(IProduitService produitService)
    {
        _produitService = produitService ?? throw new ArgumentNullException(nameof(produitService));
        AddProductCommand = new Command(async () => await AddProduitAsync());

    }


    private async Task AddProduitAsync()
    {
        try
        {
            if ( string.IsNullOrWhiteSpace(Libelle) || string.IsNullOrWhiteSpace(Qt) || string.IsNullOrWhiteSpace(Prix))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Tous les champs sont obligatoires.", "OK");
                return;
            }

            var produit = new Produits
            {
                Libelle = Libelle,
                Qt = int.Parse(Qt),
                Prix = int.Parse(Prix)
            };

            await _produitService.AddProduitAsync(produit);

            await Application.Current.MainPage.DisplayAlert("Succès", "Produit ajouté avec succès.", "OK");

            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Erreur", $"Échec de l'ajout du produit : {ex.Message}", "OK");
        }
    }
}

