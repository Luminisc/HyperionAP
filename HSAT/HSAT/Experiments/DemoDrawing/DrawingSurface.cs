namespace HSAT.Experiments.DemoDrawing
{
    public class DrawingSurface : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var random = new Random();
            int x = random.Next(500);
            int y = random.Next(500);
            canvas.StrokeColor = Color.FromRgb(0, 255, 0);
            canvas.DrawCircle(new Point(x, y), 10d);
        }
    }
}
