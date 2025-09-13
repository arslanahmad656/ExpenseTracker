using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Repository.Configuration;

public class FormStateConfiguration : IEntityTypeConfiguration<FormState>
{
    public void Configure(EntityTypeBuilder<FormState> builder)
    {
        builder.HasData(Enum.GetValues<FormStatus>()
            .Select(f => new FormState
            {
                Id = (int)f,
                State = f
            }));
    }
}
