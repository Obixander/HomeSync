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
        public DbSet<Family> families { get; set; } = null;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
