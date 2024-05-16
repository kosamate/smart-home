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


        
    }
}