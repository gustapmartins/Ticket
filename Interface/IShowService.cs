using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Show;
using Ticket.Model;

namespace Ticket.Interface;

public interface IShowService
{
    List<Show> FindAllShow();
    Show FindIdShow(int id);
    Task<List<Show>> SearchShow(string name);
    Show CreateShow(ShowCreateDto showDto);
    Show DeleteShow(int Id);
    Show UpdateShow(int Id, ShowUpdateDto showtDto);
}
