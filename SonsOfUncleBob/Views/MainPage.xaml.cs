using SonsOfUncleBob.ViewModels;

namespace SonsOfUncleBob
{

    public class GraphicsDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Drawing code goes here
            canvas.FillColor = Colors.DarkBlue;
            canvas.FillRectangle(10, 10, 100, 50);
        }
    }
    public partial class MainPage : ContentPage
    {
        int count = 0;
        bool ButtonPressed = false;
        HomeViewModel viewModel = new();

        public MainPage()
        {
            this.BindingContext = viewModel;
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