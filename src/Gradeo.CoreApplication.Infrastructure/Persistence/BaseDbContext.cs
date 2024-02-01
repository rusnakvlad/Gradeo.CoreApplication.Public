using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Infrastructure.Persistence;

public class BaseDbContext<TContext> : DbContext where TContext : DbContext
{
    public ICurrentUserService _currentUserService { get; protected set; }
    
    protected BaseDbContext(DbContextOptions<TContext> options) : base(options)
    {
    }

    /*protected BaseDbContext(
        DbContextOptions<TContext> options, 
        ICurrentUserService currentUserService
    ) : base(options)
    {
        _currentUserService = currentUserService;
    }*/
    
    public void ApplyBaseEntityConfiguration(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes().Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType)))
        {
            builder.Entity(entityType.ClrType)
               .Property<DateTimeOffset>(nameof(BaseEntity.CreatedAt)).HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Entity(entityType.ClrType)
              .Property<DateTimeOffset>(nameof(BaseEntity.LastModifiedAt)).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        }
    }

    public virtual void ApplyQueryFilters<T>(ModelBuilder builder) where T : class
    {
        if (typeof(T).IsAssignableTo(typeof(ISoftDeletable)))
        {
            builder.Entity<T>().HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ApplyBaseEntityConfiguration(builder);
        base.OnModelCreating(builder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        var result = base.SaveChanges(acceptAllChangesOnSuccess);

        return result;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        OnBeforeSaving();
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        return result;
    }

    protected virtual void OnBeforeSaving()
    {
        TrackTimeModifyStamps();
        foreach (var entry in ChangeTracker.Entries())
            switch (entry.State)
            {
                // Soft delete entity
                case EntityState.Deleted when entry.Entity is ISoftDeletable:
                    entry.State = EntityState.Unchanged;
                    entry.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
                    break;
            }
    }
    
    public virtual void TrackTimeModifyStamps()
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => (e.Entity is BaseEntity) && (e.State == EntityState.Added || e.State == EntityState.Modified)))
        {
            var item = entry.Entity as BaseEntity;
            if (item != null)
            {
                if (entry.State == EntityState.Added)
                {
                    item.CreatedAt = DateTimeOffset.UtcNow;
                    item.LastModifiedAt = DateTimeOffset.UtcNow;

                    item.CreatedBy = _currentUserService?.UserId;
                    item.LastModifiedBy = _currentUserService?.UserId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    item.LastModifiedAt = DateTimeOffset.UtcNow;
                    item.LastModifiedBy = _currentUserService?.UserId;
                }
            }
        }
    }
}