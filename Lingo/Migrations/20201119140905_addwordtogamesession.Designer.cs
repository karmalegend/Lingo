﻿// <auto-generated />
using System;
using Lingo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lingo.Migrations
{
    [DbContext(typeof(LingoContext))]
    [Migration("20201119140905_addwordtogamesession")]
    partial class addwordtogamesession
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("fiveLetterWords");
                });

            modelBuilder.Entity("Lingo.Models.gameSessionModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("Guesses")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("currentword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("lastGuess")
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

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("sevenLetterWords");
                });

            modelBuilder.Entity("Lingo.Models.sixLetterWordModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

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
