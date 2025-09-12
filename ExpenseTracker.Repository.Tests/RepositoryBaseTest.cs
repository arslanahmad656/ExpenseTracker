using AutoFixture.Xunit2;
using ExpenseTracker.Contracts;
using ExpenseTracker.TestsBase;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository.Tests;

public class RepositoryBaseTest : TestBase
{
    private static (TestEntityRepository Repository, ExpenseTrackerDbContext DbContext) GetRepository()
    {
        var options = new DbContextOptionsBuilder<ExpenseTrackerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new TestDbContext(options);
        return (new TestEntityRepository(context), context);
    }

    [Theory]
    [AutoData]
    public async Task Add_OnlyEntity_InsertsInDbContext(TestEntity entity)
    {
        var (repository, dbContext) = GetRepository();

        await repository.Add(entity);

        await dbContext.SaveChangesAsync();

        var addedEntity = await dbContext.Set<TestEntity>().FirstOrDefaultAsync(e => e.Id == entity.Id);

        addedEntity.Should().NotBeNull();
    }

    [Theory]
    [AutoData]
    public async Task Add_DuplicateEntity_ThrowsException(TestEntity entity)
    {
        var (repository, dbContext) = GetRepository();
        await repository.Add(entity);
        await dbContext.SaveChangesAsync();
        Func<Task> act = async () =>
        {
            await repository.Add(entity);
            await dbContext.SaveChangesAsync();
        };
        await act.Should().ThrowAsync<Exception>();
    }

    [Theory]
    [AutoData]
    public async Task Add_UniqueEntities_InsertsInDbContext(TestEntity entity1, TestEntity entity2)
    {
        var (repository, dbContext) = GetRepository();
        await repository.Add(entity1);
        await repository.Add(entity2);
        await dbContext.SaveChangesAsync();
        var addedEntity1 = await dbContext.Set<TestEntity>().FirstOrDefaultAsync(e => e.Id == entity1.Id);
        var addedEntity2 = await dbContext.Set<TestEntity>().FirstOrDefaultAsync(e => e.Id == entity2.Id);
        addedEntity1.Should().NotBeNull();
        addedEntity2.Should().NotBeNull();
    }

    [Theory]
    [AutoData]
    public async Task Delete_NonExistingEntity_ThrowsException(TestEntity testEntity)
    {
        var (repo, context) = GetRepository();

        Func<Task> act = async () =>
        {
            repo.Delete(testEntity);
            await context.SaveChangesAsync();
        };

        await act.Should().ThrowAsync<DbUpdateConcurrencyException>();
    }

    [Theory]
    [AutoData]
    public async Task Delete_ExistingEntity_RemovesFromContext(TestEntity testEntity)
    {
        var (repo, context) = GetRepository();

        await context.Set<TestEntity>().AddAsync(testEntity);
        await context.SaveChangesAsync();

        repo.Delete(testEntity);
        await context.SaveChangesAsync();

        var exists = await context.Set<TestEntity>().AnyAsync(e => e.Id == testEntity.Id);
        exists.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task Delete_NonTrackedEntity_RemovesFromContext(TestEntity testEntity)
    {
        var (repo, context) = GetRepository();

        await context.Set<TestEntity>().AddAsync(testEntity);
        await context.SaveChangesAsync();

        context.Entry(testEntity).State = EntityState.Detached;

        repo.Delete(testEntity);
        await context.SaveChangesAsync();

        var exists = await context.Set<TestEntity>().AnyAsync(e => e.Id == testEntity.Id);
        exists.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task Update_ExistingEntity_UpdatesInContext(TestEntity testEntity, string newName)
    {
        var (repo, context) = GetRepository();
        await context.Set<TestEntity>().AddAsync(testEntity);
        await context.SaveChangesAsync();
        testEntity.Name = newName;
        repo.Update(testEntity);
        await context.SaveChangesAsync();
        var updatedEntity = await context.Set<TestEntity>().FirstOrDefaultAsync(e => e.Id == testEntity.Id);
        updatedEntity.Should().NotBeNull();
        updatedEntity!.Name.Should().Be(newName);
    }

    [Theory]
    [AutoData]
    public async Task Update_NonExistingEntity_ThrowsException(TestEntity testEntity)
    {
        var (repo, context) = GetRepository();
        Func<Task> act = async () =>
        {
            repo.Update(testEntity);
            await context.SaveChangesAsync();
        };
        await act.Should().ThrowAsync<DbUpdateConcurrencyException>();
    }

    [Theory]
    [AutoData]
    public async Task Update_NoChange_NoChangeInTheDbContext(TestEntity testEntity)
    {
        var (repo, context) = GetRepository();
        await context.Set<TestEntity>().AddAsync(testEntity);
        await context.SaveChangesAsync();
        repo.Update(testEntity);
        
        var retrievedEntity = await context.Set<TestEntity>().FirstOrDefaultAsync(e => e.Id == testEntity.Id);
        retrievedEntity.Should().NotBeNull();
        retrievedEntity!.Name.Should().Be(testEntity.Name);
    }

    [Theory]
    [AutoData]
    public async Task Update_NonTrackedEntity_UpdatesInDbContext(TestEntity testEntity)
    {
        var (repo, context) = GetRepository();
        await context.Set<TestEntity>().AddAsync(testEntity);
        await context.SaveChangesAsync();
        context.Entry(testEntity).State = EntityState.Detached;
        var newName = "UpdatedName";
        testEntity.Name = newName;
        repo.Update(testEntity);
        await context.SaveChangesAsync();
        var updatedEntity = await context.Set<TestEntity>().FirstOrDefaultAsync(e => e.Id == testEntity.Id);
        updatedEntity.Should().NotBeNull();
        updatedEntity!.Name.Should().Be(newName);
    }

    [Theory]
    [AutoData]
    public async Task Update_ConflictingIdChange_ThrowsException(TestEntity testEntity1, TestEntity testEntity2)
    {
        var (repo, context) = GetRepository();
        await context.Set<TestEntity>().AddAsync(testEntity1);
        await context.Set<TestEntity>().AddAsync(testEntity2);
        await context.SaveChangesAsync();
        testEntity1.Id = testEntity2.Id;
        var act = () => repo.Update(testEntity1);
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public async Task Find_NoArgs_RetrievesAllData()
    {
        var (repo, context) = GetRepository();
        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" }
        };
        await context.Set<TestEntity>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
        var retrievedEntities = await repo.FindByCondition().ToListAsync();
        retrievedEntities.Should().HaveCount(3);
        retrievedEntities.Should().BeEquivalentTo(entities);
    }

    [Fact]
    public async Task Find_WithFilter_RetrievesTheFilteredData()
    {
        var (repo, context) = GetRepository();
        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" }
        };
        await context.Set<TestEntity>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
        var retrievedEntities = await repo.FindByCondition(e => e.Id > 1).ToListAsync();
        retrievedEntities.Should().HaveCount(2);
        retrievedEntities.Should().BeEquivalentTo(entities.Where(e => e.Id > 1));
    }

    [Fact]
    public async Task Find_WithNoMatchingFilter_RetrievesNoData()
    {
        var (repo, context) = GetRepository();
        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" }
        };
        await context.Set<TestEntity>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
        var retrievedEntities = await repo.FindByCondition(e => e.Id > 3).ToListAsync();
        retrievedEntities.Should().BeEmpty();
    }

    [Fact]
    public async Task Find_EmptyDbContext_RetrievesNoData()
    {
        var (repo, context) = GetRepository();
        var retrievedEntities = await repo.FindByCondition().ToListAsync();
        retrievedEntities.Should().BeEmpty();
    }

    [Fact]
    public async Task Find_WithComplexFilter_RetrievesTheFilteredData()
    {
        var (repo, context) = GetRepository();
        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Alice" },
            new() { Id = 2, Name = "Bob" },
            new() { Id = 3, Name = "Charlie" },
            new() { Id = 4, Name = "David" }
        };
        await context.Set<TestEntity>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
        var retrievedEntities = await repo.FindByCondition(e => e.Id % 2 == 0 && e.Name.Contains("a", StringComparison.OrdinalIgnoreCase)).ToListAsync();
        retrievedEntities.Should().HaveCount(1);
        retrievedEntities.Should().BeEquivalentTo(new List<TestEntity> { entities[3] });
    }

    [Fact]
    public async Task Find_WithOrderBy_RetrievesOrderedData()
    {
        var (repo, context) = GetRepository();
        var entities = new List<TestEntity>
        {
            new() { Id = 3, Name = "Charlie" },
            new() { Id = 1, Name = "Alice" },
            new() { Id = 4, Name = "David" },
            new() { Id = 2, Name = "Bob" }
        };
        await context.Set<TestEntity>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
        var retrievedEntities = await repo.FindByCondition(orderBy: q => q.OrderBy(e => e.Name)).ToListAsync();
        retrievedEntities.Should().HaveCount(4);
        retrievedEntities[0].Name.Should().Be("Alice");
        retrievedEntities[1].Name.Should().Be("Bob");
        retrievedEntities[2].Name.Should().Be("Charlie");
        retrievedEntities[3].Name.Should().Be("David");
    }

    [Fact]
    public async Task Find_WithIncludes_PopulatesTheNavigationProperties()
    {
        var (repo, context) = GetRepository();

        var parentEntity = new ParentEntity
        {
            Id = 1,
            Description = "Parent Entity"
        };

        var childEntities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Child1", ParentEntityId = parentEntity.Id },
            new() { Id = 2, Name = "Child2", ParentEntityId = parentEntity.Id }
        };

        await context.Set<ParentEntity>().AddAsync(parentEntity);
        await context.Set<TestEntity>().AddRangeAsync(childEntities);
        await context.SaveChangesAsync();

        var retrievedEntities = await repo.FindByCondition(includes: e => e.ParentEntity!).ToListAsync();

        retrievedEntities.Should().NotBeEmpty();
        retrievedEntities.All(e => e.ParentEntity != null).Should().BeTrue();
    }
}
