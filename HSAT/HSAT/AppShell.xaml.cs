using HSAT.ViewModels;

namespace HSAT;

public partial class AppShell : Shell
{
    ProjectContextViewModel ProjectContext { get; set; } = new ();

    public AppShell()
	{
		InitializeComponent();
		
	}

    private void CreateProject(object sender, EventArgs e)
    {
        
    }
}
