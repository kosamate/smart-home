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

        //Open in full screen mode:
        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            #if WINDOWS
                  var window = App.Current.Windows.FirstOrDefault().Handler.PlatformView as Microsoft.UI.Xaml.Window;
                  IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                  Microsoft.UI.WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                  Microsoft.UI.Windowing.AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
                //appWindow.SetPresenter(Microsoft.UI.Windowing.AppWindowPresenterKind.FullScreen);
                   (appWindow.Presenter as Microsoft.UI.Windowing.OverlappedPresenter).Maximize();
                // this line can maximize the window
            #endif
        }


        //public class GraphicsDrawable : IDrawable
        //{
        //    public void Draw(ICanvas canvas, RectF dirtyRect)
        //    {
        //        // Drawing code goes here
        //        canvas.FillColor = Colors.DarkBlue;
        //        canvas.FillRectangle(10, 10, 100, 50);
        //    }
        //}

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}


        //private void Btn_Pressed(System.Object sender, System.EventArgs e)
        //{
        //        CounterBtn.Background = new SolidColorBrush(Colors.LightGreen);
        //}
    }
}