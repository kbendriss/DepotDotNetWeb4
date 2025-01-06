﻿// <auto-generated />
using System;
using BasketBallLiveScore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BasketBallLiveScore.Migrations
{
    [DbContext(typeof(BasketballContext))]
    [Migration("20241219220624_RemoveBasketballGameIdFromPlayers")]
    partial class RemoveBasketballGameIdFromPlayers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BasketBallLiveScore.Models.BasketballGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Competition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GameNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuarterCount")
                        .HasColumnType("int");

                    b.Property<int>("QuarterDuration")
                        .HasColumnType("int");

                    b.Property<int>("SetupManagerId")
                        .HasColumnType("int");

                    b.Property<int?>("SpectatorCount")
                        .HasColumnType("int");

                    b.Property<int>("TimeoutDuration")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Venue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SetupManagerId");

                    b.HasIndex("UserId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.GameStaff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BasketballGameId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BasketballGameId");

                    b.ToTable("GameStaff");
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BasketballGameId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCaptain")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPlaying")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("PlayerTeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BasketballGameId");

                    b.HasIndex("PlayerTeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BasketballGameId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LongCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BasketballGameId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.TeamStaff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StaffTeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StaffTeamId");

                    b.ToTable("TeamStaff");
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.BasketballGame", b =>
                {
                    b.HasOne("BasketBallLiveScore.Models.User", null)
                        .WithMany()
                        .HasForeignKey("SetupManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BasketBallLiveScore.Models.User", null)
                        .WithMany("Games")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.GameStaff", b =>
                {
                    b.HasOne("BasketBallLiveScore.Models.BasketballGame", null)
                        .WithMany("GameStaff")
                        .HasForeignKey("BasketballGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.Player", b =>
                {
                    b.HasOne("BasketBallLiveScore.Models.BasketballGame", null)
                        .WithMany("StartingFive")
                        .HasForeignKey("BasketballGameId");

                    b.HasOne("BasketBallLiveScore.Models.Team", null)
                        .WithMany("Players")
                        .HasForeignKey("PlayerTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.Team", b =>
                {
                    b.HasOne("BasketBallLiveScore.Models.BasketballGame", null)
                        .WithMany("Teams")
                        .HasForeignKey("BasketballGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.TeamStaff", b =>
                {
                    b.HasOne("BasketBallLiveScore.Models.Team", null)
                        .WithMany("TeamStaff")
                        .HasForeignKey("StaffTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.BasketballGame", b =>
                {
                    b.Navigation("GameStaff");

                    b.Navigation("StartingFive");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.Team", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("TeamStaff");
                });

            modelBuilder.Entity("BasketBallLiveScore.Models.User", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
