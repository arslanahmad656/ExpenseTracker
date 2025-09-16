using AutoMapper;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.DataTransferObjects;

namespace ExpenseTracker.App;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Role, RoleDto>();
        CreateMap<Principal, PrincipalDto>();
        CreateMap<Currency, CurrencyDto>();
    }
}
