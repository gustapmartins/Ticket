using AutoMapper;
using Ticket.DTO.Category;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using Microsoft.AspNetCore.JsonPatch;
using Ticket.Repository;

namespace Ticket.Service;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ICategoryDao _categoryDao;

    public CategoryService(IMapper mapper, ICategoryDao categoryDao)
    {
        _mapper = mapper;
        _categoryDao = categoryDao;
    }

    public IEnumerable<Category> FindAll()
    {
        try
        {
            var find = _categoryDao.FindAllCategorys();

            if (find.Count() == 0)
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

    public Category FindId(int Id)
    {
        try
        {
            var categorys = _categoryDao.FindIdCategory(Id);

            if (categorys == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            return categorys;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public CategoryCreateDto CreateCategory(CategoryCreateDto categoryDto)
    {
        try
        {
            var categoryExist = _categoryDao.CategoryExistName(categoryDto);

            if (categoryExist)
            {
                throw new StudentNotFoundException("This category already exists");
            }

            Category category = _mapper.Map<Category>(categoryDto);

            _categoryDao.Add(category);

            return categoryDto;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Category DeleteCategory(int Id)
    {
        try
        {
            var category = _categoryDao.FindIdCategory(Id);
            if (category == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }
            _categoryDao.Remove(category);

            return category;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public CategoryUpdateDto UpdateCategory(int Id, JsonPatchDocument<CategoryUpdateDto> categoryDto)
    {
        try
        {
            var category = _categoryDao.FindIdCategory(Id);

            if (category == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            var categoryView = _mapper.Map<CategoryUpdateDto>(category);

            categoryDto.ApplyTo(categoryView);

            _mapper.Map(categoryView, category);

            _categoryDao.SaveChanges();

            return categoryView;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}