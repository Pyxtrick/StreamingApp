using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.Entities.Internal;
using System.Reflection;

namespace StreamingApp.DB;

public class UnitOfWorkContext : DbContext, IDisposable, IAsyncDisposable
{
    public UnitOfWorkContext(DbContextOptions<UnitOfWorkContext> options)
        : base(options)
    { }

    // add-migration addSettings -startup StreamingApp.Web

    // for entity type already been tracked
    //_dbContext.Achievements.AttachRange(modified.Where(r => r.Id > 0));

    public DbSet<Achievements> Achievements { get; set; } = null!;

    public DbSet<Ban> Ban { get; set; } = null!;

    public DbSet<CommandAndResponse> CommandAndResponse { get; set; } = null!;

    public DbSet<Emotes> Emotes { get; set; } = null!;

    public DbSet<EmotesCondition> EmotesCondition { get; set; } = null!;

    public DbSet<GameInfo> GameInfo { get; set; } = null!;

    public DbSet<GameStream> GameStream { get; set; } = null!;

    public DbSet<Settings> Settings { get; set; } = null!;

    public DbSet<SpecialWords> SpecialWords { get; set; } = null!;

    public DbSet<Status> Status { get; set; } = null!;

    public DbSet<StreamHistory> StreamHistory { get; set; } = null!;

    public DbSet<Sub> Sub { get; set; } = null!;

    public DbSet<User> User { get; set; } = null!;

    public DbSet<UserDetail> UserDetail { get; set; } = null!;

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        //OnBeforeSaving();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: ArgumentValidator.EnsureNotNull(modelBuilder, nameof(modelBuilder));
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    private void OnBeforeSaving()
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            Console.WriteLine(entry);
            if (entry.Entity is EntityBase entity)
            {
                entry.Property(nameof(EntityBase.UpdatedAt)).IsModified = false;

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTimeOffset.Now;
                }
            }
        }
    }
}
