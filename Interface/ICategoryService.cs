using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICategoryService
{
    List<Category> FindAllCategory();
    Task<Category> FindIdCategory(int id);
    CategoryCreateDto CreateCategory(CategoryCreateDto CategoryDto);
    Task<Category> DeleteCategory(int id);
    Task<Category> UpdateCategory(int id, CategoryUpdateDto categoryDto);
}
