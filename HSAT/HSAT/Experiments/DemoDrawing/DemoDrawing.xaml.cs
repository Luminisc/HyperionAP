namespace HSAT.Experiments.DemoDrawing;

public partial class DemoDrawing : ContentPage
{
	public DemoDrawing()
	{
		InitializeComponent();
	}

    private void Image_Tapped(object sender, EventArgs e)
    {
		graphicsView.Invalidate();
    }
}