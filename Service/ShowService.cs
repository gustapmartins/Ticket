﻿using Microsoft.EntityFrameworkCore;
using Ticket.Repository.Dao;
using Ticket.Validation;
using Ticket.Interface;
using Ticket.DTO.Show;
using Ticket.Model;
using Ticket.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticket.Commons;

namespace Ticket.Service;

public class ShowService: BaseService, IShowService
{
    private readonly IMapper _mapper;
    private readonly IShowDao _showDao;
    private readonly TicketContext _ticketContext;
    private readonly ViaCep _viacep;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ShowService(IMapper mapper, IShowDao showDao, TicketContext ticketContext, IWebHostEnvironment webHostEnvironment)
    {
        _mapper = mapper;
        _showDao = showDao;
        _viacep = new ViaCep();
        _ticketContext = ticketContext;
        _webHostEnvironment = webHostEnvironment;
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

    public byte[] GetImagem(string fileName)
    {
        try
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(path, fileName + ".png");

            if (File.Exists(filePath))
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                return fileBytes;
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ResultOperation<Show>> CreateShow(ShowCreateDto showDto)
    {
        try
        {
            Category category = _showDao.FindByCategoryName(showDto.CategoryName);

            if (category == null)
            {
                return CreateErrorResult<Show>($"This value {showDto.CategoryName} is not exist");
            }

            Show nameExist = _showDao.FindByName(showDto.Name);

            if (nameExist != null)
            {
                return CreateErrorResult<Show>("This show already exists");
            }

            var show = _mapper.Map<Show>(showDto);

            show.Category = category;
            show.Address = await GetOrCreateAddressAsync(showDto.CEP);
            show.ImagePath = SaveImage(showDto.imageFile);
            show.Date = DateTime.Now.ToUniversalTime();

            _showDao.Add(show);

            return CreateSuccessResult(show);
        }
        catch (Exception ex)
        {
            return CreateErrorResult<Show>($"{ex.Message}");
        }
    }


    private string SaveImage(IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(path, uniqueFileName);
            imageFile.CopyTo(new FileStream(filePath, FileMode.Create));

            return uniqueFileName;
        }

        return null;
    }

    private async Task<Address> GetOrCreateAddressAsync(string cep)
    {
        Address address = await _ticketContext.Address.FirstOrDefaultAsync(c => c.CEP == cep);

        if (address == null)
        {
            address = await _viacep.GetCep(cep);
        }

        return address;
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
        }catch(Exception ex)
        {
            return CreateErrorResult<Show>(ex.Message);
        }
    }
}