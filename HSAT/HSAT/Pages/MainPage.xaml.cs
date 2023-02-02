namespace HSAT;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		//count++;

		//if (count == 1)
		//	CounterBtn.Text = $"Clicked {count} time";
		//else
		//	CounterBtn.Text = $"Clicked {count} times";

		//SemanticScreenReader.Announce(CounterBtn.Text);
	}

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
		Console.WriteLine(e);
		lblLabel.Text = $"{e.GestureId}: {e.StatusType} ({e.TotalX} {e.TotalY})";
    }
}

