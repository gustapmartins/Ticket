using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Show;
using Ticket.Model;

namespace Ticket.Interface;

public interface IShowService
{
    ResultOperation<List<Show>> FindAllShow();
    ResultOperation<Show> FindIdShow(string id);
    Task<ResultOperation<Show>> CreateShow(ShowCreateDto showDto);
    ResultOperation<Show> DeleteShow(string Id);
    ResultOperation<Show> UpdateShow(string Id, ShowUpdateDto showtDto);
}
