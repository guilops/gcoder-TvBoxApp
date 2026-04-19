namespace TvBoxApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Routing.RegisterRoute(nameof(SorteioPage), typeof(SorteioPage));
        return new Window(new AppShell());
    }
}