using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Shared.Contracts;

namespace ExpenseTracker.Repository.Tests;

public class TestEntity : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    [ForeignKey(nameof(ParentEntity))]
    public int? ParentEntityId { get; set; }

    public ParentEntity? ParentEntity { get; set; }
}


public class ParentEntity : IEntity
{
    public int Id { get; set; }

    public string Description { get; set; } = "";

    public ICollection<TestEntity>? TestEntities { get; set; }
}