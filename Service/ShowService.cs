using Ticket.ExceptionFilter;
using Ticket.Repository.Dao;
using Ticket.Validation;
using Ticket.Interface;
using Ticket.DTO.Show;
using Ticket.Model;
using AutoMapper;
using Ticket.Data;
using Microsoft.EntityFrameworkCore;

namespace Ticket.Service;

public class ShowService: TicketBase, IShowService
{
    private readonly IMapper _mapper;
    private readonly IShowDao _showDao;
    private readonly TicketContext _ticketContext;
    private readonly ViaCep _viacep;

    public ShowService(IMapper mapper, IShowDao showDao, TicketContext ticketContext)
    {
        _mapper = mapper;
        _showDao = showDao;
        _viacep = new ViaCep();
        _ticketContext = ticketContext;
    }

    public List<Show> FindAllShow()
    {
        try
        {
            List<Show> show = _showDao.FindAll();

            if (show.Count == 0)
            {
                throw new StudentNotFoundException("Está lista está vazia");
            }

            return show;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Show FindIdShow(string id)
    {
        return HandleErrorAsync(() => _showDao.FindId(id));
    }

    public async Task<Show> CreateShow(ShowCreateDto showDto)
    {
        try
        {
            Category category = HandleErrorAsync(() => _showDao.FindByCategoryName(showDto.CategoryName));

            Show nameExist = _showDao.FindByName(showDto.Name);

            if (nameExist != null)
            {
                throw new StudentNotFoundException("This show already exists");
            }

            Address address = new Address();

            address = await _ticketContext.Address.FirstOrDefaultAsync(c => c.CEP == showDto.CEP);

            if(address == null)
            {
                address = await _viacep.GetCep(showDto.CEP);
            }

            var show = _mapper.Map<Show>(showDto);

            show.Category = category;
            show.Address = address;
            show.Date = DateTime.Now.ToUniversalTime();

            _showDao.Add(show);

            return show;
        }catch(Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public Show DeleteShow(string Id)
    {
        var show = HandleErrorAsync(() => _showDao.FindId(Id));

        _showDao.Remove(show);

        return show;
    }

    public Show UpdateShow(string Id, ShowUpdateDto showDto)
    {
        var show = HandleErrorAsync(() => _showDao.FindId(Id));

        _showDao.Update(show, showDto);

        return show;
    }
}