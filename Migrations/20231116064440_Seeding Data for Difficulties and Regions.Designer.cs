﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using learn.Data;

#nullable disable

namespace learn.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    [Migration("20231116064440_Seeding Data for Difficulties and Regions")]
    partial class SeedingDataforDifficultiesandRegions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("learn.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cee58370-8dc7-4f3b-b1a3-0a961be18f72"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("721f7b45-2e23-4ba5-a8bc-7b047481fd91"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("8324d18e-f798-499d-8fba-a0a80e925412"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("learn.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("44731059-8385-4260-8317-472341e74576"),
                            Code = "GRS",
                            Name = "Gresik",
                            RegionImageUrl = "https://picsum.photos/id/1/400/300"
                        },
                        new
                        {
                            Id = new Guid("55f065e4-9959-4d54-b468-f3b8688f0d63"),
                            Code = "KDR",
                            Name = "Kediri",
                            RegionImageUrl = "https://picsum.photos/id/2/400/300"
                        },
                        new
                        {
                            Id = new Guid("18c9dfa7-f68a-4344-85d8-aac12c6572cb"),
                            Code = "MDR",
                            Name = "Madura",
                            RegionImageUrl = "https://picsum.photos/id/3/400/300"
                        });
                });

            modelBuilder.Entity("learn.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uuid");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("learn.Models.Domain.Walk", b =>
                {
                    b.HasOne("learn.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("learn.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}