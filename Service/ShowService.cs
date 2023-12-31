﻿using Ticket.Repository.Dao;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.DTO.Show;
using Ticket.Model;
using AutoMapper;
using Ticket.Validation;

namespace Ticket.Service;

public class ShowService: TicketBase, IShowService
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

    public async Task<List<Show>> SearchShow(string name)
    {
        return await _showDao.FindByShowNameList(name);
    }

    public Show CreateShow(ShowCreateDto showDto)
    {
        Category category = HandleErrorAsync(() => _showDao.FindByCategoryName(showDto.CategoryName));

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