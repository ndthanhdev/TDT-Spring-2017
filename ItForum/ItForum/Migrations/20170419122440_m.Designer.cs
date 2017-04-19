using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ItForum;

namespace ItForum.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170419122440_m")]
    partial class m
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("ItForum.Models.Comment", b =>
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

            modelBuilder.Entity("ItForum.Models.Container", b =>
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

            modelBuilder.Entity("ItForum.Models.ContainerTag", b =>
                {
                    b.Property<string>("ContainerId");

                    b.Property<string>("TagName");

                    b.HasKey("ContainerId", "TagName");

                    b.HasIndex("TagName");

                    b.ToTable("ContainerTags");
                });

            modelBuilder.Entity("ItForum.Models.Point", b =>
                {
                    b.Property<string>("PostId");

                    b.Property<string>("UserId");

                    b.HasKey("PostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("ItForum.Models.Post", b =>
                {
                    b.Property<string>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContainerId");

                    b.Property<string>("Content");

                    b.Property<bool>("IsVerified");

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("UserId");

                    b.HasKey("PostId");

                    b.HasIndex("ContainerId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ItForum.Models.Tag", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Name");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ItForum.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdmissionYear");

                    b.Property<string>("Email");

                    b.Property<string>("Faculty");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsVerified");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Phone");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ItForum.Models.UserClaim", b =>
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

            modelBuilder.Entity("ItForum.Models.UserTag", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("TagName");

                    b.HasKey("UserId", "TagName");

                    b.HasIndex("TagName");

                    b.ToTable("UserTags");
                });

            modelBuilder.Entity("ItForum.Models.Comment", b =>
                {
                    b.HasOne("ItForum.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItForum.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItForum.Models.Container", b =>
                {
                    b.HasOne("ItForum.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItForum.Models.ContainerTag", b =>
                {
                    b.HasOne("ItForum.Models.Container", "Container")
                        .WithMany("ContainerTags")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItForum.Models.Tag", "Tag")
                        .WithMany("ContainerTags")
                        .HasForeignKey("TagName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItForum.Models.Point", b =>
                {
                    b.HasOne("ItForum.Models.Post", "Post")
                        .WithMany("Points")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItForum.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItForum.Models.Post", b =>
                {
                    b.HasOne("ItForum.Models.Container", "Container")
                        .WithMany("Posts")
                        .HasForeignKey("ContainerId");

                    b.HasOne("ItForum.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ItForum.Models.UserClaim", b =>
                {
                    b.HasOne("ItForum.Models.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ItForum.Models.UserTag", b =>
                {
                    b.HasOne("ItForum.Models.Tag", "Tag")
                        .WithMany("UserTags")
                        .HasForeignKey("TagName")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItForum.Models.User", "User")
                        .WithMany("UserTags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
