using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ticket.Model;

public class Address
{
    public Address()
    {
        Id = Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    public string Id { get; set; }

    [JsonPropertyName("cep")]
    public string CEP { get; set; }

    [JsonPropertyName("logradouro")]
    public string Logradouro { get; set; }

    [JsonPropertyName("complemento")]
    public string Complement { get; set; }

    [JsonPropertyName("bairro")]
    public string Neighborhood { get; set; }

    [JsonPropertyName("localidade")]
    public string Location { get; set; }

    [JsonPropertyName("uf")]
    public string UF { get; set; }
}