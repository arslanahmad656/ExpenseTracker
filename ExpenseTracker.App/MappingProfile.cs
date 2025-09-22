using AutoMapper;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Entities.Models.Views;
using ExpenseTracker.Shared.DataTransferObjects;
using ExpenseTracker.Shared.Models;

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
        CreateMap<FormGridView, FormGridSearchEntry>();
        CreateMap<FormHistory, FormHistoryRecordEntry>()
            .ForCtorParam(nameof(FormHistoryRecordEntry.ActionDate), c => c.MapFrom(h => h.RecordedDate))
            .ForCtorParam(nameof(FormHistoryRecordEntry.ActorName), c => c.MapFrom(h => h.Actor.Name))
            .ForCtorParam(nameof(FormHistoryRecordEntry.ActionType), c => c.MapFrom(h => h.Status));
    }
}
