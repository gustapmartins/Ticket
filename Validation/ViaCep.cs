using Ticket.ExceptionFilter;
using System.Text.Json;
using Ticket.Model;

namespace Ticket.Validation;

public class ViaCep
{
    public async Task<Address> GetCep(string cep)
    {
        try
        {
            string apiUrl = $"https://viacep.com.br/ws/{cep}/json/";

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Converte a resposta para um objeto EnderecoViaCep
                string responseData = await response.Content.ReadAsStringAsync();
                var endereco = JsonSerializer.Deserialize<Address>(responseData);
                return endereco;
            }
            else
            {
                // Se a requisição falhou, lança uma exceção ou retorna null, dependendo da sua lógica
                return null;
            }
        }catch (Exception ex)
        {
            throw new StudentNotFoundException(ex.Message, ex);
        }
    }
}
