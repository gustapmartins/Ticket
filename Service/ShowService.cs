using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

    public ShowService(IShowDao showDao, IMapper mapper)
    {
        _showDao = showDao;
        _mapper = mapper;
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

    public ShowCreateDto CreateShow(ShowCreateDto showDto)
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

        _showDao.Add(show);
        return showDto;

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

    public ShowUpdateDto UpdateShow(int Id, JsonPatchDocument<ShowUpdateDto> showDto)
    {
        try
        {
            Show show = _showDao.FindId(Id);

            if (show == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            var showView = _mapper.Map<ShowUpdateDto>(show);

            showDto.ApplyTo(showView);

            _mapper.Map(showView, show);

            _showDao.SaveChanges();

            return showView;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}