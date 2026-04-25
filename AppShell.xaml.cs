namespace TvBoxApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(TrilhaCristaPage), typeof(TrilhaCristaPage));
        Routing.RegisterRoute(nameof(SorteioPage), typeof(SorteioPage));
    }
}
