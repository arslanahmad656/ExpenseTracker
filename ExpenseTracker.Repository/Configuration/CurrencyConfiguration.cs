using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Repository.Configuration;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasData([
            new() { Id = 1, Code = "USD", FullName = "US Dollar", Symbol = "$" },
            new() { Id = 2, Code = "EUR", FullName = "Euro", Symbol = "€" },
            new() { Id = 3, Code = "GBP", FullName = "British Pound", Symbol = "£" },
            new() { Id = 4, Code = "INR", FullName = "Indian Rupee", Symbol = "₹" },
            new Currency() { Id = 5, Code = "PKR", FullName = "Pakistani Rupee", Symbol = "₨" }
        ]);
    }
}
