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
        public DbSet<CustomList> CustomLists { get; set; } = null;
        public DbSet<CustomListItem> CustomListItems { get; set; } = null;
        public DbSet<Family> Families { get; set; } = null;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasMany(a => a.AssignedMembers)
                .WithMany(u => u.Activities);

            modelBuilder.Entity<User>().HasMany(u => u.Activities).WithMany(a => a.AssignedMembers);

            modelBuilder.Entity<CustomList>()
                 .HasMany(c => c.Items) // CustomList has many CustomListItems
                 .WithOne(ci => ci.CustomList) // Each CustomListItem is related to one CustomList
                 .HasForeignKey(ci => ci.CustomListId) // CustomListId is the foreign key
                 .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete of items when a list is deleted

            base.OnModelCreating(modelBuilder);
        }

    }
}
