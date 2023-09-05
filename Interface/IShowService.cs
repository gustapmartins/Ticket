using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Show;
using Ticket.Model;

namespace Ticket.Interface;

public interface IShowService
{
    IEnumerable<Show> FindAll();

    Show FindId(int id);

    ShowCreateDto CreateShow(ShowCreateDto showDto);

    Show DeleteShow(int Id);

    ShowUpdateDto UpdateShow(int id, JsonPatchDocument<ShowUpdateDto> showDto);
}
