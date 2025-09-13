using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Repository.Configuration;

public class ExpenseStateConfiguration : IEntityTypeConfiguration<ExpenseState>
{
    public void Configure(EntityTypeBuilder<ExpenseState> builder)
    {
        builder.HasData(Enum.GetValues<ExpenseStatus>()
            .Select(e => new ExpenseState
            {
                Id = (int)e,
                State = e
            }));
    }
}
