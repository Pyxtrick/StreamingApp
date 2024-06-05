﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StreamingApp.DB;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    [DbContext(typeof(UnitOfWorkContext))]
    [Migration("20240316112624_updateData")]
    partial class updateData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Achievements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GiftedBitsCount")
                        .HasColumnType("int");

                    b.Property<int>("GiftedDonationCount")
                        .HasColumnType("int");

                    b.Property<int>("GiftedSubsCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastStreamSeen")
                        .HasColumnType("datetime2");

                    b.Property<int>("WachedStreams")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Ban", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BanedAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BanedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBaned")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMessageBaned")
                        .HasColumnType("bit");

                    b.Property<string>("LastMessage")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("MessagesDeletedAmount")
                        .HasColumnType("int");

                    b.Property<int>("TimeOutAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ban");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.CommandAndResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Auth")
                        .HasColumnType("int");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("CommandAndResponse");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Emotes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMute")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Sound")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("TimesUsed")
                        .HasColumnType("int");

                    b.Property<byte[]>("Video")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Emotes");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.GameInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Game")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameCategory")
                        .HasColumnType("int");

                    b.Property<string>("GameId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameInfo");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.GameStream", b =>
                {
                    b.Property<int>("GameCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameCategoryId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StreamId")
                        .HasColumnType("int");

                    b.HasKey("GameCategoryId");

                    b.ToTable("GameStream");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AllChat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AllertDelayS")
                        .HasColumnType("int");

                    b.Property<bool>("ComunityDayActive")
                        .HasColumnType("bit");

                    b.Property<string>("Delay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MuteAllerts")
                        .HasColumnType("bit");

                    b.Property<int>("SpamAmmount")
                        .HasColumnType("int");

                    b.Property<int>("TimeOutSeconds")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.SpecialWords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimesUsed")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SpecialWords");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FallowDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FirstChatDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsVIP")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<int>("TwitchSubId")
                        .HasColumnType("int");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TwitchSubId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.StreamHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameStreamId")
                        .HasColumnType("int");

                    b.Property<string>("StreamTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StreamHistory");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Sub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentTier")
                        .HasColumnType("int");

                    b.Property<bool>("CurrentySubscribed")
                        .HasColumnType("bit");

                    b.Property<bool>("SubGiffted")
                        .HasColumnType("bit");

                    b.Property<int>("SubscribedTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sub");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BanId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("TwitchAchievementsId")
                        .HasColumnType("int");

                    b.Property<int>("TwitchDetailId")
                        .HasColumnType("int");

                    b.Property<string>("UserText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Status", b =>
                {
                    b.HasOne("StreamingApp.Domain.Entities.Internal.Sub", "TwitchSub")
                        .WithMany()
                        .HasForeignKey("TwitchSubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TwitchSub");
                });
#pragma warning restore 612, 618
        }
    }
}
