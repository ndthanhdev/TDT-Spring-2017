using ItForum.Models;
using Microsoft.EntityFrameworkCore;

namespace ItForum
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        //public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Point> Points { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }


        // Mapping two one to many
        public DbSet<UserTag> UserTags { get; set; }

        public DbSet<ContainerTag> ContainerTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Point>()
                .HasKey(model => new {model.PostId, model.UserId});

            // many-to-many Container and Tag
            modelBuilder.Entity<ContainerTag>()
                .HasKey(model => new {model.ContainerId, model.TagId});

            // many-to-many User and Tag
            modelBuilder.Entity<UserTag>()
                .HasKey(model => new {model.UserId, model.TagId});
        }
    }
}