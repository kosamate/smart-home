using SonsOfUncleBob.ViewModels;
using SonsOfUncleBob.Commands;
using System.Windows.Input;

namespace SonsOfUncleBob
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        bool ButtonPressed = false;

        public MainPage(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

        }
        
    }
}