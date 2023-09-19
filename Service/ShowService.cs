using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Ticket.Data;
using Ticket.DTO.Show;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Service;

public class ShowService: IShowService
{
    private readonly TicketContext _ticketContext;
    private readonly IMapper _mapper;

    public ShowService(TicketContext ticketContext, IMapper mapper)
    {
        _ticketContext = ticketContext;
        _mapper = mapper;
    }

    public List<Show> FindAllShow()
    {
        try
        {
            var show = _ticketContext.Shows.ToList();

            if (show.Count == 0)
            {
                throw new StudentNotFoundException("Está lista está vazia");
            }

            return show;
        }catch(Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Show FindIdShow(int id)
    {
        try
        {
            var show = _ticketContext.Shows.FirstOrDefault(category => category.Id == id);

            if (show == null)
            {
                throw new StudentNotFoundException("The list is empty");
            }

            return show;
        }
        catch(Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public ShowCreateDto CreateShow(ShowCreateDto showDto)
    {
        try
        {
            var category = _ticketContext.Categorys.FirstOrDefault(category =>
                category.Name == showDto.CategoryName);

            var nameExist = _ticketContext.Shows.FirstOrDefault(show => show.Name == showDto.Name);

            if (category == null)
            {
                throw new StudentNotFoundException("The specified category does not exist.");
            }

            if(nameExist != null)
            {
                throw new StudentNotFoundException("This show already exists");
            }

            var show = new Show
            {
                Name = showDto.Name,
                Description = showDto.Description,
                Price = showDto.Price,
                Date = showDto.Date,
                Local = showDto.Local,
                Category = category
            };

            _ticketContext.Shows.Add(show);
            _ticketContext.SaveChanges();
            return showDto;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Show DeleteShow(int Id)
    {
        try
        {
            var show = _ticketContext.Shows.FirstOrDefault(show => show.Id == Id);

            if (show == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }
            _ticketContext.Remove(show);
            _ticketContext.SaveChanges();
            return show;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public ShowUpdateDto UpdateShow(int Id, JsonPatchDocument<ShowUpdateDto> showDto)
    {
        try
        {
            var show = _ticketContext.Shows.FirstOrDefault(show => show.Id == Id);

            if (show == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            var showView = _mapper.Map<ShowUpdateDto>(show);

            showDto.ApplyTo(showView);

            _mapper.Map(showView, show);
            _ticketContext.SaveChanges();
            return showView;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}