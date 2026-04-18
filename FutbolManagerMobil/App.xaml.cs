namespace FutbolManagerMobil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            _ = Veritabani.JSONVerileriniYukleAsync();
            // Windows boyutlandırma kodun burada kalabilir
            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
#if WINDOWS
                var nativeWindow = handler.PlatformView;
                nativeWindow.Activate();
                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
                appWindow.Resize(new Windows.Graphics.SizeInt32(1280, 800));
#endif
            });
        }

        // BURASI KRİTİK: Uygulamanın hangi sayfayla başlayacağını buradan yönetiyoruz
        protected override Window CreateWindow(IActivationState? activationState)
        {
            // AppShell yerine doğrudan LoadingPage ile başlat
            return new Window(new NavigationPage(new LoadingPage()));
        }
    }
}