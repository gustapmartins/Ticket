using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICategoryService
{
    List<Category> FindAllCategory();
    Category FindIdCategory(int id);
    CategoryCreateDto CreateCategory(CategoryCreateDto CategoryDto);
    Category DeleteCategory(int id);
    Category UpdateCategory(int id, CategoryUpdateDto categoryDto);
}
