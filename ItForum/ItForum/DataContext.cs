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

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostPoint> PostPoints { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentPoint> CommentPoints { get; set; }



        // Mapping two one to many
        public DbSet<UserTag> UserTags { get; set; }

        public DbSet<TopicTag> ContainerTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostPoint>()
                .HasKey(model => new { model.PostId, model.UserId });

            modelBuilder.Entity<CommentPoint>()
                .HasKey(cp => new { cp.CommentId, cp.UserId });

            // many-to-many Topic and Tag
            modelBuilder.Entity<TopicTag>()
                .HasKey(model => new { ContainerId = model.TopicId, model.TagName });

            // many-to-many User and Tag
            modelBuilder.Entity<UserTag>()
                .HasKey(model => new { model.UserId, model.TagName });
        }
    }
}