using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICategoryService
{
    ResultOperation<List<Category>> FindAllCategory();
    ResultOperation<Category> FindIdCategory(string id);
    ResultOperation<CategoryCreateDto> CreateCategory(CategoryCreateDto CategoryDto);
    ResultOperation<Category> DeleteCategory(string id);
    ResultOperation<Category> UpdateCategory(string id, CategoryUpdateDto categoryDto);
}
