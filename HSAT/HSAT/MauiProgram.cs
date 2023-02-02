using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Windows.Graphics;

namespace HSAT;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.ConfigureLifecycleEvents(lifecycle =>
        {
            lifecycle.AddWindows(windows => windows.OnWindowCreated((window) =>
            {
                window.ExtendsContentIntoTitleBar = true;
                window.Title = "HSAT";

                IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                if (winuiAppWindow.Presenter is OverlappedPresenter p)
                {
                    //p.Maximize();
                }
                else
                {
                    const int width = 1280;
                    const int height = 720;
                    winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
                }
            }));
        });

        return builder.Build();
    }
}
