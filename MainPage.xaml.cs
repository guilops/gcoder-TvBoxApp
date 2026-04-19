using AndroidX.CardView.Widget;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace TvBoxApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // TV Box: foco inicial seguro (Border funciona melhor que ContentView)
        CardYouTube?.Focus();
    }

    private void Animate(CardView view)
    {
        // safe animation fallback
    }

    private async void OpenLink(string url)
    {
        await Launcher.OpenAsync(url);
    }

    private void TapYouTube(object sender, EventArgs e)
        => OpenLink("https://youtube.com");

    private void TapPrime(object sender, EventArgs e)
        => OpenLink("https://primevideo.com");

    private void TapNetflix(object sender, EventArgs e)
        => OpenLink("https://netflix.com");

    private void TapDisney(object sender, EventArgs e)
        => OpenLink("https://disneyplus.com");

    private async void TapSorteio(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SorteioPage));
    }

    private async void TapContato(object sender, EventArgs e)
    {
        await Launcher.OpenAsync("mailto:guilhermelopes_dev@hotmail.com");
    }
}