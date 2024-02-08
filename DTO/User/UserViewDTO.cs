using Ticket.Model;

namespace Ticket.DTO.User;

public class UserViewDTO
{
    public string Id { get; set; }
    public string UserName {  get; set; }
    public string Email {  get; set; }
    public string Cpf { get; set; }
    public int YearsOld { get; set; }
    public string Role { get; set; }
}
