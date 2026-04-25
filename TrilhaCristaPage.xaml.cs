using Microsoft.Maui.Media;

namespace TvBoxApp;

public partial class TrilhaCristaPage : ContentPage
{
    List<string> frases = new()
{
    "O Senhor é a minha força e o meu refúgio em todo tempo.",
    "Deus nunca chega atrasado, Ele chega no tempo certo.",
    "Aquele que começou a boa obra em você é fiel para completar.",
    "Confie no Senhor de todo o coração e não se apoie no seu próprio entendimento.",
    "Deus é o nosso socorro bem presente nas tribulações.",
    "Maior é aquele que está com você do que qualquer dificuldade.",
    "Espere no Senhor, seja forte e corajoso.",
    "A paz de Deus guarda o coração e a mente.",
    "O choro pode durar uma noite, mas a alegria vem pela manhã.",
    "Quem anda com Deus nunca anda sozinho.",
    "Deus abre portas que ninguém pode fechar.",
    "Tudo coopera para o bem daqueles que amam a Deus.",
    "A fé move o impossível.",
    "Deus cuida de cada detalhe da sua vida.",
    "Não temas, Deus é contigo por onde você andar.",
    "A presença de Deus transforma qualquer situação.",
    "Deus renova suas forças como as da águia.",
    "Ele é fiel para cumprir todas as promessas.",
    "Deus é especialista em recomeços.",
    "Quando você não puder, Deus pode.",
    "A graça de Deus é suficiente.",
    "Deus está no controle mesmo quando tudo parece perdido.",
    "A vitória vem do Senhor.",
    "Deus nunca falha.",
    "Ele sustenta você todos os dias.",
    "O amor de Deus nunca acaba.",
    "Deus é refúgio seguro em tempos difíceis.",
    "Nada pode te separar do amor de Deus.",
    "Deus transforma dor em propósito.",
    "O Senhor luta por você.",
    "Deus conhece seu coração.",
    "Ele vê cada lágrima.",
    "Deus honra quem permanece fiel.",
    "O Senhor é a sua fortaleza.",
    "Deus te levanta mesmo quando você cai.",
    "Ele faz o impossível acontecer.",
    "Deus nunca te abandona.",
    "Sua vitória está chegando.",
    "Deus abre caminhos no deserto.",
    "Ele é a luz no meio da escuridão.",
    "Deus restaura o que foi perdido.",
    "O Senhor é bom o tempo todo.",
    "Deus tem planos maiores do que você imagina.",
    "Ele cuida até dos detalhes invisíveis.",
    "Deus responde orações.",
    "Nada é impossível para Deus.",
    "Deus te escolheu e te chamou.",
    "Ele é o seu escudo e proteção.",
    "Deus fortalece o cansado.",
    "O Senhor te guia pelo melhor caminho"
};

    List<string> pastores = new()
{
    "Claudio Duarte",
    "Deive Leonardo",
    "Silas Malafaia",
    "Marco Feliciano",
    "Valdemiro Santiago",
    "Edir Macedo",
    "R.R. Soares",
    "Hernandes Dias Lopes",
    "Samuel Mariano",
    "Anderson Freire",
    "Davi Sacer",
    "Bruno Leonardo",
    "Josué Gonçalves",
    "Lucinho Barreto",
    "Elizeu Rodrigues",
    "Abner Ferreira",
    "Silas Daniel",
    "Robson Rodovalho",
    "Estevam Hernandes",
    "Sonia Hernandes",
    "Paulo Junior",
    "Coty Guimarães",
    "Eli Soares",
    "Leonardo Sale",
    "Clodomir Santos",
    "Jorge Linhares",
    "José Wellington Bezerra da Costa",
    "José Wellington Junior",
    "Esequias Soares",
    "Altair Germano",
    "Elinaldo Renovato",
    "Antonio Gilberto",
    "Gedeão Santos",
    "Sebastião Rodrigues",
    "Daniel Berg (histórico)",
    "Gunnar Vingren (histórico)",
    "Billy Graham",
    "T.D. Jakes",
    "Joel Osteen",
    "John Piper",
    "Paul Washer",
    "A.R. Bernard",
    "Benny Hinn",
    "Reinhard Bonnke",
    "David Wilkerson",
    "Creflo Dollar",
    "Joyce Meyer",
    "Steven Furtick",
    "Craig Groeschel",
    "Tony Evans",
    "Jhonathan Carlos",
    "Elizeu Rodrigues",
};

    string fraseAtual;

    public TrilhaCristaPage()
    {
        InitializeComponent();
        ListaPastores.ItemsSource = pastores;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var random = new Random();
        fraseAtual = frases[random.Next(frases.Count)];

        LabelFrase.Text = fraseAtual;

        await TextToSpeech.SpeakAsync(fraseAtual);
    }

    private async void OnFalarFrase(object sender, EventArgs e)
    {
        await TextToSpeech.SpeakAsync(fraseAtual);
    }

    private void OnPastorSelecionado(object sender, SelectionChangedEventArgs e)
    {
        var nome = e.CurrentSelection.FirstOrDefault()?.ToString();

        if (nome == null) return;

        var query = $"{nome} pregação";
        var url = $"https://www.youtube.com/results?search_query={Uri.EscapeDataString(query)}";

        Launcher.OpenAsync(url);
    }

    private void OnFinalizarDia(object sender, EventArgs e)
    {
        int pontos = 0;

        if (chkBiblia.IsChecked) pontos++;
        if (chkJejum.IsChecked) pontos++;
        if (chkCulto.IsChecked) pontos++;
        if (chkOracao.IsChecked) pontos++;
        if (chkVigilia.IsChecked) pontos++;

        if (pontos >= 4)
            Resultado.Text = $"🔥 Mandou bem! Você fez {pontos} coisas hoje!";
        else if (pontos >= 2)
            Resultado.Text = $"👍 Bom, mas dá pra melhorar! ({pontos})";
        else
            Resultado.Text = $"💪 Vamos com mais foco amanhã! ({pontos})";
    }
}