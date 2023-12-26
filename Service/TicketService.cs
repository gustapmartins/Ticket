using Ticket.ExceptionFilter;
using Ticket.Repository.Dao;
using Ticket.DTO.Ticket;
using Ticket.Interface;
using Ticket.Model;
using AutoMapper;
using Ticket.Validation;

namespace Ticket.Service;

public class TicketService: BaseService, ITicketService
{
    private readonly IMapper _mapper;
    private readonly ITicketDao _ticketDao;
    public TicketService(
        ITicketDao ticketDao, 
        IMapper mapper)
    {
        _mapper = mapper;
        _ticketDao = ticketDao;
    }

    public ResultOperation<List<Tickets>> FindAllTicket()
    {
        try
        {
            List<Tickets> ticket = _ticketDao.FindAll();

            if (ticket.Count == 0)
            {
                return CreateErrorResult<List<Tickets>>("The list is empty");
            }

            return CreateSuccessResult(ticket);
        }
        catch (Exception ex)
        {
            return CreateErrorResult<List<Tickets>>(ex.Message);
        }
    }

    public ResultOperation<Tickets> FindIdTicket(string id)
    {

        var findTicket = _ticketDao.FindId(id);

        if(findTicket == null)
        {
            return CreateErrorResult<Tickets>("This value is not exist");
        }

        return CreateSuccessResult(findTicket);
    }

    public async Task<List<Show>> SearchShow(string name)
    {
        return await _ticketDao.FindByShowNameList(name);
    }

    public ResultOperation<Tickets> CreateTicket(TicketCreateDto ticketDto)
    {
        try
        {
            Show show = _ticketDao.FindByShowName(ticketDto.ShowName);

            if (show == null)
            {
                return CreateErrorResult<Tickets>("This value is not exist");
            }

            Tickets ticket = _mapper.Map<Tickets>(ticketDto);
            ticket.Show = show;
            _ticketDao.Add(ticket);

            return CreateSuccessResult(ticket);
        }
        catch(Exception ex)
        {
            return CreateErrorResult<Tickets>(ex.Message);
        }
    }

    public ResultOperation<Tickets> DeleteTicket(string Id)
    {
        try
        {
            var ticket = _ticketDao.FindId(Id);

            if(ticket == null)
            {
                return CreateErrorResult<Tickets>("This value is not exist");
            }

            _ticketDao.Remove(ticket);

            return CreateSuccessResult(ticket);
        }
        catch(Exception ex)
        {
            return CreateErrorResult<Tickets>(ex.Message);
        }
    }

    public ResultOperation<Tickets> UpdateTicket(string Id, TicketUpdateDto ticketDto)
    {
        try
        {
            var ticket = _ticketDao.FindId(Id);

            if(ticket == null)
            {
                return CreateErrorResult<Tickets>("This value is not exist");
            }

            _ticketDao.Update(ticket, ticketDto);

            return CreateSuccessResult(ticket);
        }catch(Exception ex)
        {
            return CreateErrorResult<Tickets>(ex.Message);
        }
    }
}
