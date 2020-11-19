﻿// <auto-generated />
using System;
using Lingo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lingo.Migrations
{
    [DbContext(typeof(LingoContext))]
    partial class LingoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Lingo.Models.fiveLetterWordModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("word")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.HasIndex("word")
                        .IsUnique()
                        .HasFilter("[word] IS NOT NULL");

                    b.ToTable("fiveLetterWords");
                });

            modelBuilder.Entity("Lingo.Models.gameSessionModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int?>("Guesses")
                        .HasColumnType("int");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<string>("currentword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("lastGuess")
                        .HasColumnType("datetime2");

                    b.Property<long?>("playerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("playerId");

                    b.ToTable("gameSessions");
                });

            modelBuilder.Entity("Lingo.Models.sevenLetterWordModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("word")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("Id");

                    b.HasIndex("word")
                        .IsUnique()
                        .HasFilter("[word] IS NOT NULL");

                    b.ToTable("sevenLetterWords");
                });

            modelBuilder.Entity("Lingo.Models.sixLetterWordModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("word")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.HasKey("Id");

                    b.HasIndex("word")
                        .IsUnique()
                        .HasFilter("[word] IS NOT NULL");

                    b.ToTable("sixLetterWords");
                });

            modelBuilder.Entity("Lingo.Models.userModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gameSessionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Lingo.Models.gameSessionModel", b =>
                {
                    b.HasOne("Lingo.Models.userModel", "player")
                        .WithMany()
                        .HasForeignKey("playerId");

                    b.Navigation("player");
                });
#pragma warning restore 612, 618
        }
    }
}
