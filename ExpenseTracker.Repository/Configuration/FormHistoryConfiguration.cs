using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Repository.Configuration;

public class FormHistoryConfiguration : IEntityTypeConfiguration<FormHistory>
{
    public void Configure(EntityTypeBuilder<FormHistory> builder)
    {
        
    }
}
