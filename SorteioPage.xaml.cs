using System.Text.Json;

namespace TvBoxApp;

public partial class SorteioPage : ContentPage
{
    public SorteioPage()
    {
        InitializeComponent();
        CarregarHistorico();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        QuantidadePessoasEntry?.Focus();
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private readonly List<string> tarefasAdulto = new()
    {
        "Lavar louça","Lavar banheiro","Lavar roupa","Estender roupa",
        "Guardar roupa","Arrumar cama","Varrer casa","Passar pano",
        "Tirar lixo","Limpar geladeira","Limpar fogão","Fazer comida",
        "Lavar quintal","Organizar sala","Limpar janelas","Passar roupa",
        "Limpar microondas","Organizar armário","Cuidar das plantas",
        "Limpar banheiro completo","Trocar roupa de cama","Lavar carro"
    };

    private readonly List<string> tarefasCrianca = new()
    {
        "Estudar","Fazer lição","Arrumar material","Arrumar cama",
        "Organizar brinquedos","Ler um livro","Guardar mochila",
        "Ajudar na mesa","Guardar roupas","Limpar quarto",
        "Organizar sapatos","Ajudar na cozinha","Separar lixo",
        "Revisar caderno","Arrumar estante","Dobrar roupas",
        "Guardar brinquedos","Limpar sala","Fazer desenho",
        "Ler história","Organizar livros","Ajudar irmão"
    };

    private readonly List<string> emojis = new() { "😂", "🤣", "😆", "😹", "🤪", "😜", "😝" };

    // GERAR CAMPOS
    private void OnGerarCamposClicked(object sender, EventArgs e)
    {
        NomesLayout.Children.Clear();

        if (!int.TryParse(QuantidadePessoasEntry.Text, out int qtd) || qtd <= 0)
            return;

        for (int i = 0; i < qtd; i++)
        {
            var linha = new HorizontalStackLayout { Spacing = 10 };

            linha.Children.Add(new Entry
            {
                Placeholder = "Nome",
                WidthRequest = 200,
                TextColor = Colors.White,
                PlaceholderColor = Colors.Gray
            });

            linha.Children.Add(new Entry
            {
                Placeholder = "Idade",
                Keyboard = Keyboard.Numeric,
                WidthRequest = 100,
                TextColor = Colors.White,
                PlaceholderColor = Colors.Gray
            });

            NomesLayout.Children.Add(linha);
        }
    }

    // SORTEIO
    private async void OnSortearClicked(object sender, EventArgs e)
    {
        ResultadoLayout.Children.Clear();

        var pessoas = NomesLayout.Children
            .OfType<HorizontalStackLayout>()
            .Select(l =>
            {
                var entries = l.Children.OfType<Entry>().ToList();

                if (entries.Count < 2)
                    return null;

                return new
                {
                    Nome = entries[0].Text,
                    Idade = int.TryParse(entries[1].Text, out int idade) ? idade : 0
                };
            })
            .Where(p => p != null && !string.IsNullOrWhiteSpace(p.Nome))
            .ToList();

        if (pessoas.Count == 0)
            return;

        var random = new Random();
        var resultadoFinal = new List<string>();

        for (int i = 0; i < 15; i++)
        {
            var temp = pessoas[random.Next(pessoas.Count)];
            var listaTemp = temp.Idade < 18 ? tarefasCrianca : tarefasAdulto;
            var tarefaTemp = listaTemp[random.Next(listaTemp.Count)];

            AnimacaoLabel.Text = $"{temp.Nome} - {tarefaTemp}";
            await Task.Delay(80);
        }

        AnimacaoLabel.Text = "";

        foreach (var pessoa in pessoas)
        {
            var lista = pessoa.Idade < 18 ? tarefasCrianca : tarefasAdulto;
            var tarefa = lista[random.Next(lista.Count)];
            var emoji = emojis[random.Next(emojis.Count)];

            var texto = $"{pessoa.Nome} - {tarefa} {emoji}";
            resultadoFinal.Add(texto);

            ResultadoLayout.Children.Add(new Label
            {
                Text = texto,
                FontSize = 20,
                TextColor = Colors.White
            });
        }

        await TextToSpeech.SpeakAsync("Confira o resultado do sorteio!");

        SalvarHistorico(resultadoFinal);
        CarregarHistorico();
    }

    // HISTÓRICO
    private void SalvarHistorico(List<string> resultado)
    {
        var json = Preferences.Get("historico", "");

        var lista = string.IsNullOrEmpty(json)
            ? new List<HistoricoItem>()
            : JsonSerializer.Deserialize<List<HistoricoItem>>(json) ?? new();

        lista.Insert(0, new HistoricoItem
        {
            Data = DateTime.Now,
            Resultados = resultado
        });

        if (lista.Count > 5)
            lista.RemoveAt(lista.Count - 1);

        Preferences.Set("historico", JsonSerializer.Serialize(lista));
    }

    private void CarregarHistorico()
    {
        HistoricoLayout.Children.Clear();

        var json = Preferences.Get("historico", "");
        if (string.IsNullOrEmpty(json)) return;

        var lista = JsonSerializer.Deserialize<List<HistoricoItem>>(json) ?? new();

        foreach (var item in lista)
        {
            var stack = new VerticalStackLayout();

            stack.Children.Add(new Label
            {
                Text = item.Data.ToString("dd/MM/yyyy HH:mm:ss"),
                TextColor = Colors.Yellow,
                FontSize = 14
            });

            foreach (var r in item.Resultados)
            {
                stack.Children.Add(new Label
                {
                    Text = r,
                    TextColor = Colors.Gray,
                    FontSize = 14
                });
            }

            HistoricoLayout.Children.Add(stack);
        }
    }
}

public class HistoricoItem
{
    public DateTime Data { get; set; }
    public List<string> Resultados { get; set; } = new();
}