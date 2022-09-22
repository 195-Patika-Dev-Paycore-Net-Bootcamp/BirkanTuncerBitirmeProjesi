using AutoMapper;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<AccountDto, Account>().ReverseMap();

            CreateMap<ProductDto, Product>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<OfferDto, Offer>().ReverseMap();

            CreateMap<OrderDto, Order>().ReverseMap();

            CreateMap<EmailRabbitMQDto, EmailRabbitMQ>().ReverseMap();
        }

    }
}
