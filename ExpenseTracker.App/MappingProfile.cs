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
            .ForCtorParam(nameof(ExpenseDto.LastUpdatedOn), c => c.MapFrom(_ => default(DateTimeOffset)))
            .ForCtorParam(nameof(ExpenseDto.RejectionReason), c => c.MapFrom(_ => string.Empty));
        CreateMap<Form, FormDto>()
            .ForCtorParam(nameof(FormDto.LastUpdatedOn), c => c.MapFrom(_ => default(DateTimeOffset)))
            .ForCtorParam(nameof(FormDto.RejectionReason), c => c.MapFrom(_ => string.Empty));
    }
}
