namespace SonsOfUncleBob
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 1480;
            const int newHeight = 830;

            window.MinimumHeight = window.MaximumHeight = window.Height = newHeight;
            window.MinimumWidth = window.MaximumWidth = window.Width = newWidth;

            window.X = -6;
            window.Y = 0;

            return window;
        }

    }
}
