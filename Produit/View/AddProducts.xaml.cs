using Produit.View_Model;

namespace Produit.View;

public partial class AddProducts : ContentPage
{
    private readonly AddProductViewModel _viewModel;

    public AddProducts()
	{
        InitializeComponent();
       
    }
    public AddProducts(AddProductViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

}