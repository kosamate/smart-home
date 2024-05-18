using SonsOfUncleBob.ViewModels;
using SonsOfUncleBob.Commands;
using System.Windows.Input;

namespace SonsOfUncleBob
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