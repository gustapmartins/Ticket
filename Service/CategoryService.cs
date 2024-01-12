using Ticket.DTO.Category;
using Ticket.Repository.Dao;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using AutoMapper;
using Ticket.Validation;

namespace Ticket.Service;

public class CategoryService : BaseService, ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ICategoryDao _categoryDao;

    public CategoryService(IMapper mapper,  ICategoryDao categoryDao)
    {
        _mapper = mapper;
        _categoryDao = categoryDao;
    }

    public ResultOperation<List<Category>> FindAllCategory()
    {
        try
        {
            List<Category> findCategory = _categoryDao.FindAll();

            if (findCategory.Count == 0)
            {
                return CreateErrorResult<List<Category>>("The list is empty");
            }

            return CreateSuccessResult(findCategory);
        }
        catch (Exception ex)
        {
            return CreateErrorResult<List<Category>>(ex.Message);
        }
    }

    public ResultOperation<Category> FindIdCategory(string Id)
    {

        try
        {
            var findCategoryId = _categoryDao.FindId(Id);

            if (findCategoryId == null)
            {
                return CreateErrorResult<Category>("This value is not exist");
            }

            return CreateSuccessResult(findCategoryId);
        }catch(Exception ex)
        {
            return CreateErrorResult<Category>(ex.Message);
        }
    }

    public ResultOperation<CategoryCreateDto> CreateCategory(CategoryCreateDto categoryDto)
    {
        try
        {
            Category categoryExist = _categoryDao.FindByName(categoryDto.Name);

            if (categoryExist != null)
            {
                return CreateErrorResult<CategoryCreateDto>("This category already exists");
            }

            Category category = _mapper.Map<Category>(categoryDto);

            _categoryDao.Add(category);

            return CreateSuccessResult(categoryDto);
        }catch(Exception ex)
        {
            return CreateErrorResult<CategoryCreateDto>(ex.Message);
        }
    }

    public ResultOperation<Category> DeleteCategory(string Id)
    {
        try
        {
            var findCategoryId =  _categoryDao.FindId(Id);

            if(findCategoryId == null)
            {
                return CreateErrorResult<Category>("This value is not exist");
            }

            _categoryDao.Remove(findCategoryId);

            return CreateSuccessResult(findCategoryId);
        }
        catch (Exception ex)
        {
            return CreateErrorResult<Category>(ex.Message);
        }
    }

    public ResultOperation<Category> UpdateCategory(string Id, CategoryUpdateDto categoryDto)
    {
        try
        {
            var findCategoryId = _categoryDao.FindId(Id);

            if(findCategoryId == null)
            {
                return CreateErrorResult<Category>("This value is not exist");
            }

            _categoryDao.Update(findCategoryId, categoryDto);

            return CreateSuccessResult(findCategoryId);
        }
        catch (Exception ex)
        {
            return CreateErrorResult<Category>(ex.Message);
        }
    }
}