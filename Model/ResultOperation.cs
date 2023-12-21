namespace Ticket.Model;

public class ResultOperation<T>
{
    public bool Sucesso { get; set; }
    public T Dados { get; set; }
    public string MensagemErro { get; set; }
}
