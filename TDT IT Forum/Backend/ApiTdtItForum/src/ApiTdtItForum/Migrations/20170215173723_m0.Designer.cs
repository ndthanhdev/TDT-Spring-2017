using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ApiTdtItForum.Models;

namespace ApiTdtItForum.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170215173723_m0")]
    partial class m0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

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

                    b.Property<string>("AuthorUserId");

                    b.Property<string>("Content");

                    b.HasKey("PostId");

                    b.HasIndex("AuthorUserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleName");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("RoleId");

                    b.Property<string>("UserName");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
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
                    b.HasOne("ApiTdtItForum.Models.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorUserId");
                });

            modelBuilder.Entity("ApiTdtItForum.Models.User", b =>
                {
                    b.HasOne("ApiTdtItForum.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });
        }
    }
}
