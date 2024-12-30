﻿// <auto-generated />
using System;
using Identity.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Identity.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241031164207_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Identity.DAL.Entities.Permission", b =>
                {
                    b.Property<string>("PermissionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Tên quyền");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("ntext")
                        .HasColumnName("Mô tả");

                    b.HasKey("PermissionId");

                    b.ToTable("permission");
                });

            modelBuilder.Entity("Identity.DAL.Entities.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Tên vai trò");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("ntext")
                        .HasColumnName("Mô tả");

                    b.HasKey("RoleId");

                    b.ToTable("role");
                });

            modelBuilder.Entity("Identity.DAL.Entities.RolePermission", b =>
                {
                    b.Property<string>("PermissionId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("role-permission");
                });

            modelBuilder.Entity("Identity.DAL.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("Ngày tạo")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2")
                        .HasColumnName("Ngày sinh");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("user");
                });

            modelBuilder.Entity("Identity.DAL.Entities.UserRole", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("user-role");
                });

            modelBuilder.Entity("Identity.DAL.Entities.RolePermission", b =>
                {
                    b.HasOne("Identity.DAL.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity.DAL.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Identity.DAL.Entities.UserRole", b =>
                {
                    b.HasOne("Identity.DAL.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Identity.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
