

namespace SonsOfUncleBob
{

   
    public partial class MainPage : ContentPage
    {
        int count = 0;
        bool ButtonPressed = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }


        private void Btn_Pressed(System.Object sender, System.EventArgs e)
        {
                CounterBtn.Background = new SolidColorBrush(Colors.LightGreen);
        }
    }
}