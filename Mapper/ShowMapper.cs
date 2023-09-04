﻿using AutoMapper;
using Ticket.DTO.Show;
using Ticket.DTO.User;
using Ticket.Model;

namespace Ticket.Mapper;

public class ShowMapper: Profile
{
    public ShowMapper()
    {
        CreateMap<ShowCreateDto, Show>()
           .ForMember(filmeDto => filmeDto.Category, opt => opt.MapFrom(filme => filme.Category));
        CreateMap<Show, ShowViewDto>();

    }
}