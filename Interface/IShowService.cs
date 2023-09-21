using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Show;
using Ticket.Model;

namespace Ticket.Interface;

public interface IShowService
{
    List<Show> FindAllShow();

    Show FindIdShow(int id);

    ShowCreateDto CreateShow(ShowCreateDto showDto);

    Show DeleteShow(int Id);

    ShowUpdateDto UpdateShow(int Id, JsonPatchDocument<ShowUpdateDto> showtDto);
}
