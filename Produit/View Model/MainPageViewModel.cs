using System.Collections.ObjectModel;
using System.ComponentModel;
using Produit.Entities;
using Produit.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Produit.View_Model
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public readonly IProduitService _produitService;
        public ObservableCollection<Produits> _produits;
        public string _errorMessage;
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }
        private Produits _editingProduit;
        public Produits EditingProduit
        {
            get => _editingProduit;
            set => SetProperty(ref _editingProduit, value);
        }

        public ICommand OnButtonClickedCommand { get; }
        public MainPageViewModel(IProduitService produitService)
        {
            _produitService = produitService ?? throw new ArgumentNullException(nameof(produitService));
            Produits = new ObservableCollection<Produits>();
            LoadProduitsAsync();
            OnButtonClickedCommand = new Command<string>(async (buttonName) => await OnButtonClicked(buttonName));
            DeleteCommand = new Command<int>(async (idProd) => await DeleteProduitAsync(idProd));
            EditCommand = new Command<Produits>(async (produit) => await EditProduitAsync(produit));
        }

        public ObservableCollection<Produits> Produits
        {
            get => _produits;
            set
            {
                if (_produits != value)
                {
                    _produits = value;
                    OnPropertyChanged(nameof(Produits));
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public async Task LoadProduitsAsync()
        {
            try
            {
                var produits = await _produitService.GetProduitsAsync();

                Produits = produits.Any()
                    ? new ObservableCollection<Produits>(produits)
                    : new ObservableCollection<Produits>();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Une erreur s'est produite : {ex.Message}";
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task OnButtonClicked(string buttonName)
        {
            switch (buttonName)
            {
                case "AddProduct":
                    await NavigateToAddPage();
                    break;
                default:
                    throw new ArgumentException("Button not recognized");
            }
        }
        private async Task NavigateToAddPage()
        {
            await Shell.Current.GoToAsync("//AddProducts");
        }
        private async Task DeleteProduitAsync(int idProd)
        {
            try
            {
                await _produitService.DeleteProduitAsync(idProd);
          
                await Application.Current.MainPage.DisplayAlert("Succès", "Produit supprimé avec succès.", "OK");
               
                await LoadProduitsAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", $"Erreur lors de la suppression : {ex.Message}", "OK");
            }
        }

        private async Task EditProduitAsync(Produits produit)
        {
            if (produit != null)
            {
                try
                {
                    IsEditing = true;
                    EditingProduit = produit;

                    await Shell.Current.GoToAsync("//AddProducts");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", $"Erreur lors de la préparation de la modification : {ex.Message}", "OK");
                }
            }
        }

        protected bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


    }
}

