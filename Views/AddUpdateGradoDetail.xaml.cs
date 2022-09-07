using SQLiteDemo.ViewModels;

namespace SQLiteDemo.Views;

public partial class AddUpdateGradoDetail : ContentPage
{
    public AddUpdateGradoDetail(AddUpdateGradoDetailViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
    private void LimpiarGrado(object sender, EventArgs e)
    {
        
    }

}