using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ApiTdtItForum;

namespace ApiTdtItForum.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170228164829_m0")]
    partial class m0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("ApiTdtItForum.Models.Comment", b =>
                {
                    b.Property<string>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("PostId")
                        .IsRequired();

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.Container", b =>
                {
                    b.Property<string>("ContainerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PostId")
                        .IsRequired();

                    b.Property<string>("Title");

                    b.HasKey("ContainerId");

                    b.HasIndex("PostId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.ContainerTag", b =>
                {
                    b.Property<string>("ContainerId");

                    b.Property<string>("TagId");

                    b.HasKey("ContainerId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ContainerTags");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.Point", b =>
                {
                    b.Property<string>("PostId");

                    b.Property<string>("UserId");

                    b.HasKey("PostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.Post", b =>
                {
                    b.Property<string>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContainerId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("UserId");

                    b.HasKey("PostId");

                    b.HasIndex("ContainerId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdmissionYear");

                    b.Property<string>("Faculty");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsVerified");

                    b.Property<string>("Mail");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Phone");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.UserClaim", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.UserTag", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("TagId");

                    b.HasKey("UserId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("UserTags");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.Comment", b =>
                {
                    b.HasOne("ApiTdtItForum.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApiTdtItForum.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApiTdtItForum.Models.Container", b =>
                {
                    b.HasOne("ApiTdtItForum.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApiTdtItForum.Models.ContainerTag", b =>
                {
                    b.HasOne("ApiTdtItForum.Models.Container", "Container")
                        .WithMany("ContainerTags")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApiTdtItForum.Models.Tag", "Tag")
                        .WithMany("ContainerTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApiTdtItForum.Models.Point", b =>
                {
                    b.HasOne("ApiTdtItForum.Models.Post", "Post")
                        .WithMany("Points")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApiTdtItForum.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApiTdtItForum.Models.Post", b =>
                {
                    b.HasOne("ApiTdtItForum.Models.Container", "Container")
                        .WithMany("Posts")
                        .HasForeignKey("ContainerId");

                    b.HasOne("ApiTdtItForum.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.UserClaim", b =>
                {
                    b.HasOne("ApiTdtItForum.Models.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.UserTag", b =>
                {
                    b.HasOne("ApiTdtItForum.Models.Tag", "Tag")
                        .WithMany("UserTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApiTdtItForum.Models.User", "User")
                        .WithMany("UserTags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
