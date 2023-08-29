﻿using AutoMapper;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Mapper;

public class CategoryMapper: Profile
{
    public CategoryMapper() 
    {
        CreateMap<CategoryCreateDTO, Category>();
        CreateMap<CategoryUpdateDTO, Category>();
        CreateMap<Category, CategoryUpdateDTO>();
    }    

}
