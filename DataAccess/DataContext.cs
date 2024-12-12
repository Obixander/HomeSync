using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; } = null;
        public DbSet<Activity> Activities { get; set; } = null;
        public DbSet<CustomListItem> CustomListItems { get; set; } = null;
        public DbSet<CustomList> CustomLists { get; set; } = null;
        public DbSet<Family> Families { get; set; } = null;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasMany(a => a.AssignedMembers)
                .WithMany(u => u.Activities);

            modelBuilder.Entity<User>().HasMany(u => u.Activities).WithMany(a => a.AssignedMembers);

            //modelBuilder.Entity<Activity>()
            //    .HasMany(e => e.AssignedMembers)
            //    .WithMany(e => e.Activities)
            //    .UsingEntity(
            //    "ActivityUsers",
            //    l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UserId").HasPrincipalKey(nameof(User.UserId)),
            //    r => r.HasOne(typeof(Activity)).WithMany().HasForeignKey("ActivityId").HasPrincipalKey(nameof(Activity.ActivityId)),
            //    j => j.HasKey("UserId", "ActivityId"));
        }

    }
}
