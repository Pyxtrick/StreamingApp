using Microsoft.EntityFrameworkCore;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Entities.Internal.Settings;
using StreamingApp.Domain.Entities.Internal.Stream;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Entities.Internal.User;
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

    //Settings
    public DbSet<Settings> Settings { get; set; } = null!;

    //Stream
    public DbSet<Choice> Choice { get; set; }
    
    public DbSet<GameInfo> GameInfo { get; set; } = null!;

    public DbSet<Pole> Pole { get; set; } = null!;

    public DbSet<Domain.Entities.Internal.Stream.Stream> StreamHistory { get; set; } = null!;

    public DbSet<StreamGame> StreamGame { get; set; } = null!;

    public DbSet<StreamHighlight> StreamHighlights { get; set; } = null!;

    //Trigger
    public DbSet<Alert> Alert { get; set; } = null!;

    public DbSet<CommandAndResponse> CommandAndResponse { get; set; } = null!;

    public DbSet<SpecialWords> SpecialWords { get; set; } = null!;

    public DbSet<Target> Target { get; set; }

    public DbSet<TargetData> TargetData { get; set; }

    public DbSet<Trigger> Trigger { get; set; }

    //User
    public DbSet<Achievements> Achievements { get; set; } = null!;

    public DbSet<Ban> Ban { get; set; } = null!;

    public DbSet<Status> Status { get; set; } = null!;

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
        
        /** combining two objects on one table
        modelBuilder.Entity<ObjectBase>()
            .HasDiscriminator<string>("object_type")
            .HasValue<Object>("object_base")
            .HasValue<ObjectWithX>("object_x");
        */
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
