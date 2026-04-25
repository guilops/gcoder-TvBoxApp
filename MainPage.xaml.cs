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

        // Define foco inicial
        CardCristao?.Focus();

        // Mapeia clique do controle remoto (ENTER)
        AddTvClick(CardCristao, async () => await Shell.Current.GoToAsync(nameof(TrilhaCristaPage)));
        /*AddTvClick(CardYouTube, () => OpenLink("https://youtube.com"));
        AddTvClick(CardPrime, () => OpenLink("https://primevideo.com"));
        AddTvClick(CardNetflix, () => OpenLink("https://netflix.com"));
        AddTvClick(CardDisney, () => OpenLink("https://disneyplus.com"));*/
        AddTvClick(CardSorteio, async () => await Shell.Current.GoToAsync(nameof(SorteioPage)));
        AddTvClick(CardContato, async () => await Launcher.OpenAsync("mailto:guilhermelopes_dev@hotmail.com"));
    }

    // Abre links externos
    private async Task OpenLink(string url)
    {
        try
        {
            await Launcher.OpenAsync(url);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Não foi possível abrir: {url}\n{ex.Message}", "OK");
        }
    }

    // Método que conecta o clique da TV ao seu card
    private void AddTvClick(View view, Action action)
    {
        view.HandlerChanged += (s, e) =>
        {
#if ANDROID
            if (view.Handler?.PlatformView is Android.Views.View nativeView)
            {
                nativeView.Focusable = true;
                nativeView.FocusableInTouchMode = true;
                nativeView.Clickable = true;

                nativeView.SetOnClickListener(new ClickListener(action));
            }
#endif
        };
    }

    // Listener nativo do Android (captura ENTER do controle)
    class ClickListener : Java.Lang.Object, Android.Views.View.IOnClickListener
    {
        private readonly Action _action;

        public ClickListener(Action action)
        {
            _action = action;
        }

        public void OnClick(Android.Views.View v)
        {
            _action?.Invoke();
        }
    }
}