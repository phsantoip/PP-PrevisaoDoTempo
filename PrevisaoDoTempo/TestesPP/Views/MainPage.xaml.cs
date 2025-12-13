using TestesPP.ViewModels;

namespace TestesPP.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new WeatherViewModel();
    }
}
