using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace CurrencyConverterDesktop
{
    public partial class MainWindow : Window
    {
        private const string ApiBaseUrl = "https://localhost:7273";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(AmountTextBox.Text, out double amount))
            {
                string result = await ConvertCurrencyAsync(amount);
                ResultTextBlock.Text = result;
            }
            else
            {
                ResultTextBlock.Text = "Invalid input. Please enter a valid number.";
            }
        }

        private async Task<string> ConvertCurrencyAsync(double amount)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    
                    HttpResponseMessage response = await client.GetAsync($"{ApiBaseUrl}/api/currency/{amount}");

                    if (response.IsSuccessStatusCode)
                    {
                        
                        string content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<string>(content);
                    }
                    else
                    {
                        return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
