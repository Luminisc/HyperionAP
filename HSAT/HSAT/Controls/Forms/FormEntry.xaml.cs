using Maui.BindableProperty.Generator.Core;
	
namespace HSAT.Controls.Forms;

public partial class FormEntry : ContentView
{
	[AutoBindable]
	public string entryText;
    [AutoBindable]
    public string text;

	public FormEntry()
	{
		InitializeComponent();
	}
}