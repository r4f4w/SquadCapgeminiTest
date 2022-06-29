using AutoMapper;
using SquadCapgeminiTest.Dtos;
using SquadCapgeminiTest.Entities;

namespace SquadCapgeminiTest.MappingsProfiles
{
    public class CustomerCardMappings : Profile
    {
        public CustomerCardMappings()
        {
            CreateMap<CustomerCardEntity, CreateCustomerCardDto>().ReverseMap();
            CreateMap<CustomerCardEntity, ValidateCustomerCardDto>().ReverseMap();
        }
    }
}
