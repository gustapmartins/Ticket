using AutoMapper;
using Ticket.DTO.Show;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using Ticket.Repository.Dao;

namespace Ticket.Service;

public class ShowService: IShowService
{
    private readonly IMapper _mapper;
    private readonly IShowDao _showDao;

    public ShowService(IMapper mapper, IShowDao showDao)
    {
        _mapper = mapper;
        _showDao = showDao;
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
        }catch(Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Show FindIdShow(int id)
    {
        try
        {
            Show show = _showDao.FindId(id);

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

    public Show CreateShow(ShowCreateDto showDto)
    {
        Category category = _showDao.FindByCategoryName(showDto.CategoryName);

        if (category == null)
        {
            throw new StudentNotFoundException("The specified category does not exist.");
        }

        Show nameExist = _showDao.FindByName(showDto.Name);

        if (nameExist != null)
        {
            throw new StudentNotFoundException("This show already exists");
        }

        var show = _mapper.Map<Show>(showDto);

        show.Category = category;
        show.Date = DateTime.Now.ToUniversalTime();

        _showDao.Add(show);

        return show;

    }

    public Show DeleteShow(int Id)
    {
        try
        {
            Show show = _showDao.FindId(Id);

            if (show == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            _showDao.Remove(show);
            
            return show;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Show UpdateShow(int Id, ShowUpdateDto showDto)
    {
        try
        {
            Show show = _showDao.FindId(Id);

            if (show == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            _showDao.Update(show, showDto);

            return show;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}