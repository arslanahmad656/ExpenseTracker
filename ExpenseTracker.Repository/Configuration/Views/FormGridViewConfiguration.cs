using ExpenseTracker.Entities.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Repository.Configuration.Views;

public class FormGridViewConfiguration : IEntityTypeConfiguration<FormGridView>
{
    public void Configure(EntityTypeBuilder<FormGridView> builder)
    {
        builder
            .HasNoKey()
            .ToView("vw_FormGrid");
    }
}
