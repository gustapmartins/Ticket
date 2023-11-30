using Ticket.DTO.Category;
using Ticket.Repository.Dao;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using AutoMapper;
using Ticket.Validation;

namespace Ticket.Service;

public class CategoryService : TicketBase, ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ICategoryDao _categoryDao;

    public CategoryService(IMapper mapper,  ICategoryDao categoryDao)
    {
        _mapper = mapper;
        _categoryDao = categoryDao;
    }

    public List<Category> FindAllCategory()
    {
        try
        {
            List<Category> find = _categoryDao.FindAll();

            if (find.Count == 0)
            {
                throw new StudentNotFoundException("The list is empty");
            }

            return find;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Category FindIdCategory(string Id)
    {
        return HandleErrorAsync(() => _categoryDao.FindId(Id));
    }

    public CategoryCreateDto CreateCategory(CategoryCreateDto categoryDto)
    {
        Category categoryExist = _categoryDao.FindByName(categoryDto.Name);

        if (categoryExist != null)
        {
            throw new StudentNotFoundException("This category already exists");
        }

        Category category = _mapper.Map<Category>(categoryDto);

        _categoryDao.Add(category);

        return categoryDto;
    }

    public Category DeleteCategory(string Id)
    {
        try
        {
            var category = HandleErrorAsync(() =>  _categoryDao.FindId(Id));

            _categoryDao.Remove(category);

            return category;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Category UpdateCategory(string Id, CategoryUpdateDto categoryDto)
    {
        try
        {
            var category = HandleErrorAsync(() => _categoryDao.FindId(Id));

            _categoryDao.Update(category, categoryDto);

            return category;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}