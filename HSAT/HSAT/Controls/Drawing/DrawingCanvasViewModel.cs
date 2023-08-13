using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Input;
using SkiaSharp;

namespace HSAT.Controls.Drawing
{
    internal partial class DrawingCanvasViewModel : ObservableObject, IDisposable
    {
        public event EventHandler OnSurfaceInvalidation;

        [ObservableProperty]
        SKBitmap bitmap;
        [ObservableProperty]
        SKPoint currentTranslation = new SKPoint(0, 0);
        [ObservableProperty]
        SKPoint prevTranslation = new SKPoint(0, 0);
        [ObservableProperty]
        double pictureScale = 1.0f;

        public DrawingCanvasViewModel() { }

        /// <summary>Set new bitmap for canvas, and dispose previous one</summary>
        public void SetBitmap(SKBitmap bitmap)
        {
            Bitmap?.Dispose();
            Bitmap = bitmap;
            InvalidateSurface();
        }

        public void CanvasPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    CurrentTranslation = PrevTranslation + new SKPoint((float)e.TotalX, (float)e.TotalY);
                    break;
                case GestureStatus.Completed:
                    PrevTranslation = CurrentTranslation;
                    break;
                case GestureStatus.Canceled:
                    CurrentTranslation = PrevTranslation;
                    break;
            }
            InvalidateSurface();
        }

        [RelayCommand]
        public void ZoomChanged(PointerRoutedEventArgs e)
        {
            PictureScale += e.GetCurrentPoint(null).Properties.MouseWheelDelta / 1000.0f;
            PictureScale = Math.Clamp(PictureScale, double.Epsilon, 1000);
            InvalidateSurface();
        }

        private void InvalidateSurface()
        {
            OnSurfaceInvalidation?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Bitmap?.Dispose();
            Bitmap = null;
            GC.SuppressFinalize(this);
        }
    }
}
