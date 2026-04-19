using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

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

        // foco inicial (importante pra TV)
        CardYouTube.Focus();
    }

    private void OnCardFocusChanged(object sender, FocusEventArgs e)
    {
        if (sender is ContentView card)
        {
            if (e.IsFocused)
            {
                card.ScaleTo(1.1, 100);
            }
            else
            {
                card.ScaleTo(1.0, 100);
            }
        }
    }

    private async void OpenLink(string url)
    {
        await Launcher.OpenAsync(url);
    }

    private void TapYouTube(object sender, EventArgs e) => OpenLink("https://youtube.com");
    private void TapPrime(object sender, EventArgs e) => OpenLink("https://primevideo.com");
    private void TapNetflix(object sender, EventArgs e) => OpenLink("https://netflix.com");
    private void TapDisney(object sender, EventArgs e) => OpenLink("https://disneyplus.com");

    private async void TapSorteio(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SorteioPage));
    }

    private async void TapContato(object sender, EventArgs e)
    {
        await Launcher.OpenAsync("mailto:guilhermelopes_dev@hotmail.com");
    }
}