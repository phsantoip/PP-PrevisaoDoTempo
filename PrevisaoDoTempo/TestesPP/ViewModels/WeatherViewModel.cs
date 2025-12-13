using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TestesPP.Models;
using TestesPP.Services;

namespace TestesPP.ViewModels
{
    public partial class WeatherViewModel : ObservableObject
    {
        private readonly BrasilApiService brasilApi = new();
        private readonly WeatherApiService weatherApi = new();

        [ObservableProperty]
        private string cep;

        [ObservableProperty]
        private string cidadeEstado;

        [ObservableProperty]
        private Clima climaHoje;

        public ObservableCollection<Clima> Previsoes { get; } = new();

        public ICommand BuscarCommand => new Command(async () => await Buscar());

        private async Task Buscar()
        {
            
            var cepLimpo = Regex.Replace(Cep ?? "", @"\D", "");

            if (cepLimpo.Length != 8)
            {
                await Shell.Current.DisplayAlert(
                    "Erro",
                    "Digite um CEP válido (ex: 01001-000).",
                    "OK");
                return;
            }

            Cep = cepLimpo;

            try
            {
                
                var cidade = await brasilApi.GetCityByCep(Cep);

                if (string.IsNullOrWhiteSpace(cidade))
                {
                    await Shell.Current.DisplayAlert(
                        "Erro",
                        "CEP não encontrado.",
                        "OK");
                    return;
                }

                CidadeEstado = cidade;

                var previsao = await weatherApi.GetForecast(cidade);

                Previsoes.Clear();

                if (previsao == null || previsao.Count == 0)
                {
                    await Shell.Current.DisplayAlert(
                        "Erro",
                        "Não foi possível obter a previsão do tempo.",
                        "OK");
                    return;
                }

                foreach (var p in previsao)
                    Previsoes.Add(p);

                
                ClimaHoje = Previsoes.FirstOrDefault();
            }
            catch
            {
                await Shell.Current.DisplayAlert(
                    "Erro",
                    "Erro ao buscar previsão. Verifique sua conexão.",
                    "OK");
            }
        }
    }
}
