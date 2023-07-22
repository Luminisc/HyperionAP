using Microsoft.Maui.Graphics;
using SkiaSharp;

namespace HSAT.Experiments.DemoDrawing
{
    public class DrawingSurface : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var random = new Random();
            int x = random.Next((int)dirtyRect.Width);
            int y = random.Next((int)dirtyRect.Height);
            canvas.StrokeColor = Color.FromRgb(0, 255, 0);
            canvas.DrawCircle(new Point(x, y), random.NextDouble() * 50d + 10d);
        }
    }
}
