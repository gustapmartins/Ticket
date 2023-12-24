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

public class ShowService: BaseService, IShowService
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

    public ResultOperation<List<Show>> FindAllShow()
    {
        try
        {
            List<Show> show = _showDao.FindAll();

            if (show.Count == 0)
            {
                return CreateErrorResult<List<Show>>("This list is empty");
            }

            return CreateSuccessResult(show);
        }
        catch (Exception ex)
        {
            return CreateErrorResult<List<Show>>(ex.Message);
        }
    }

    public ResultOperation<Show> FindIdShow(string id)
    {
        try
        {
            var showFindId = _showDao.FindId(id);

            if(showFindId == null)
            {
                return CreateErrorResult<Show>("This is value is not exist");
            }

            return CreateSuccessResult(showFindId);
        }catch(Exception ex)
        {
            return CreateErrorResult<Show>(ex.Message);
        }
    }

    public async Task<ResultOperation<Show>> CreateShow(ShowCreateDto showDto)
    {
        try
        {
            Category category = _showDao.FindByCategoryName(showDto.CategoryName);

            if(category == null)
            {
                return CreateErrorResult<Show>($"This value {showDto.CategoryName} is not exist");
            }

            Show nameExist = _showDao.FindByName(showDto.Name);

            if (nameExist != null)
            {
                return CreateErrorResult<Show>("This show already exists");
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

            return CreateSuccessResult(show);
        }catch(Exception ex)
        {
            return CreateErrorResult<Show>($"{ex.Message}");
        }
    }

    public ResultOperation<Show> DeleteShow(string Id)
    {
        try
        {
            var show = _showDao.FindId(Id);

            if(show == null)
            {
                return CreateErrorResult<Show>("this value is not exist");
            }

            _showDao.Remove(show);

            return CreateSuccessResult(show);
        }catch(Exception ex)
        {
            return CreateErrorResult<Show>(ex.Message);
        }
    }

    public ResultOperation<Show> UpdateShow(string Id, ShowUpdateDto showDto)
    {
        try
        {
            var show = _showDao.FindId(Id);

            if(show == null)
            {
                return CreateErrorResult<Show>("This value is not exist");
            }

            _showDao.Update(show, showDto);

            return CreateSuccessResult(show);
        }catch( Exception ex)
        {
            return CreateErrorResult<Show>(ex.Message);
        }
    }
}