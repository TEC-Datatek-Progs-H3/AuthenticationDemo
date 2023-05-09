﻿// <auto-generated />
using AuthenticationDemoAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthenticationDemoAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230508104124_AddedHeroTable")]
    partial class AddedHeroTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuthenticationDemoAPI.Database.Entites.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<short>("DebutYear")
                        .HasColumnType("smallint");

                    b.Property<string>("HeroName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("RealName")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Hero");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DebutYear = (short)1938,
                            HeroName = "Superman",
                            Place = "Metropolis",
                            RealName = "Clark Kent"
                        },
                        new
                        {
                            Id = 2,
                            DebutYear = (short)1963,
                            HeroName = "Iron Man",
                            Place = "Malibu",
                            RealName = "Tony Stark"
                        });
                });

            modelBuilder.Entity("AuthenticationDemoAPI.Database.Entites.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "albert@mail.dk",
                            Password = "Test1234",
                            Role = 0,
                            Username = "Albert"
                        },
                        new
                        {
                            Id = 2,
                            Email = "benny@mail.dk",
                            Password = "Test1234",
                            Role = 1,
                            Username = "Benny"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
