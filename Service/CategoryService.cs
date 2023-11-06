using AutoMapper;
using Ticket.DTO.Category;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using Ticket.Repository.Dao;

namespace Ticket.Service;

public class CategoryService : TicketBase, ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ICategoryDao _categoryDao;
    private readonly IMessagePublisher _messagePublisher;

    public CategoryService(IMapper mapper, ICategoryDao categoryDao, IMessagePublisher messagePublisher)
    {
        _mapper = mapper;
        _categoryDao = categoryDao;
        _messagePublisher = messagePublisher;
    }

    public List<Category> FindAllCategory()
    {
        try
        {
            _messagePublisher.Publish("Hello");

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

    public async Task<Category> FindIdCategory(int Id)
    {
        return await HandleErrorAsync(async () => await _categoryDao.FindId(Id));
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

    public async Task<Category> DeleteCategory(int Id)
    {
        try
        {
            var category = await HandleErrorAsync(async () => await _categoryDao.FindId(Id));

            _categoryDao.Remove(category);

            return category;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public async Task<Category> UpdateCategory(int Id, CategoryUpdateDto categoryDto)
    {
        try
        {
            var category = await HandleErrorAsync(async () => await _categoryDao.FindId(Id));

            _categoryDao.Update(category, categoryDto);

            return category;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}