﻿// <auto-generated />
using System;
using EcoLudicoAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcoLudicoAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250429171643_AddUserFavoriteSchoolsTable")]
    partial class AddUserFavoriteSchoolsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EcoLudicoAPI.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.FavoriteProject", b =>
                {
                    b.Property<int>("FavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("FavoriteId"));

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteProjects");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<int>("AgeRange")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrls")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MaterialsList")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<string>("Tutorial")
                        .HasColumnType("longtext");

                    b.HasKey("ProjectId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.School", b =>
                {
                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("SchoolId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("OperatingHours")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("SchoolId");

                    b.HasIndex("AddressId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("DateBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("SchoolId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("AddressId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserFavoriteSchools", b =>
                {
                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SchoolId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFavoriteSchools");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.Comment", b =>
                {
                    b.HasOne("EcoLudicoAPI.Models.Project", "Project")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoLudicoAPI.Models.User", "User")
                        .WithMany("MadeComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.FavoriteProject", b =>
                {
                    b.HasOne("EcoLudicoAPI.Models.Project", "Projeto")
                        .WithMany("Favoritos")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoLudicoAPI.Models.User", "User")
                        .WithMany("FavoriteProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Projeto");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.Project", b =>
                {
                    b.HasOne("EcoLudicoAPI.Models.School", "School")
                        .WithMany("Projects")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.School", b =>
                {
                    b.HasOne("EcoLudicoAPI.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.User", b =>
                {
                    b.HasOne("EcoLudicoAPI.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoLudicoAPI.Models.School", "School")
                        .WithMany("Teachers")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Address");

                    b.Navigation("School");
                });

            modelBuilder.Entity("UserFavoriteSchools", b =>
                {
                    b.HasOne("EcoLudicoAPI.Models.School", null)
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoLudicoAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.Project", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Favoritos");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.School", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("EcoLudicoAPI.Models.User", b =>
                {
                    b.Navigation("FavoriteProjects");

                    b.Navigation("MadeComments");
                });
#pragma warning restore 612, 618
        }
    }
}
