using Microsoft.Maui.Controls;

namespace TvBoxApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

	private void OnCardFocusChanged(object sender, FocusEventArgs e)
	{
		if (sender is Frame card)
		{
			if (e.IsFocused)
			{
				card.ScaleTo(1.10, 120, Easing.SinOut);

				// Borda neon ao focar
				card.BorderColor = Color.FromArgb("#00A8FF");
				// Frame does not have a BorderWidth property in .NET MAUI; if you need a visible border thickness
				// wrap this Frame in a Border control or adjust Padding to simulate thickness.

				// Glow neon aumentando o brilho
				card.Shadow = new Shadow
				{
					Brush = new SolidColorBrush(Color.FromArgb("#00A8FF")),
					Offset = new Point(0, 8),
					Radius = 25,
					//Opacity = 0.50
				};
			}
			else
			{
				card.ScaleTo(1.0, 120, Easing.SinOut);
				card.BorderColor = Colors.Transparent;
				// Frame does not have a BorderWidth property in .NET MAUI; if you need to remove a visible border thickness
				// ensure the surrounding Border control has its StrokeThickness set to 0 or adjust Padding accordingly.

				// Retorna sombra suave do tema
				card.Shadow = new Shadow
				{
					Brush = new SolidColorBrush(Color.FromArgb("#00A8FF")),
					Offset = new Point(0, 5),
					Radius = 15,
					//Opacity = 0.35
				};
			}
		}
	}

    // Abertura dos links
    private async void OpenLink(string url)
    {
        await Launcher.OpenAsync(url);
    }

    private void TapYouTube(object sender, EventArgs e) => OpenLink("https://youtube.com");
    private void TapPrime(object sender, EventArgs e) => OpenLink("https://primevideo.com");
    private void TapNetflix(object sender, EventArgs e) => OpenLink("https://netflix.com");
    private void TapDisney(object sender, EventArgs e) => OpenLink("https://disneyplus.com");

    private async void TapContato(object sender, EventArgs e)
    {
        await Launcher.OpenAsync("mailto:guilherme.glsantos@gmail.com?subject=Contato%20TV%20Box");
    }
}