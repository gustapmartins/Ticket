namespace Ticket.Model;

public class ResultOperation<T>
{
    public bool Sucesso { get; set; }
    public T Content { get; set; }
    public string MensagemErro { get; set; }
}
