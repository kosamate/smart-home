using SmartHome.ViewModels;
using SmartHome.Commands;
using System.Windows.Input;

namespace SmartHome
{
    public partial class MainPage : ContentPage
    {

        public MainPage(HomeViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
        
    }
}