using AutoMapper;
using Ticket.DTO.Cart;
using Ticket.Model;

namespace Ticket.Mapper;

public class CartMapper: Profile
{
    public CartMapper()
    {
        CreateMap<CreateCartDto, CartItem>()
           .ForMember(dest => dest.Ticket, opt => opt.MapFrom(src => new Tickets { Id = src.TicketId }));
            

        CreateMap<Carts, CartViewDto>()
             .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Users.Id));
    }
}
