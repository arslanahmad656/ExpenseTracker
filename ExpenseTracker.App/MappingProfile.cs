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
        CreateMap<Expense, ExpenseDto>()
            .ForCtorParam(nameof(ExpenseDto.LastUpdatedOn), c => c.MapFrom(_ => default(DateTimeOffset)));
        CreateMap<Form, FormDto>()
            .ForCtorParam(nameof(ExpenseDto.LastUpdatedOn), c => c.MapFrom(_ => default(DateTimeOffset)));
    }
}
