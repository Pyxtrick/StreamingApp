﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StreamingApp.DB;

#nullable disable

namespace StreamingApp.DB.Migrations
{
    [DbContext(typeof(UnitOfWorkContext))]
    partial class UnitOfWorkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Settings.Settings", b =>
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

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpamAmmount")
                        .HasColumnType("int");

                    b.Property<int>("TimeOutSeconds")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.Choice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BitsVotes")
                        .HasColumnType("int");

                    b.Property<int>("ChannelPointsVotes")
                        .HasColumnType("int");

                    b.Property<int>("PoleId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Votes")
                        .HasColumnType("int");

                    b.Property<int>("VotesPoints")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PoleId");

                    b.ToTable("Choice");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.GameInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Game")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameInfo");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.Pole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsPole")
                        .HasColumnType("bit");

                    b.Property<string>("PoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pole");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.Stream", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("StreamEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StreamStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("StreamTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VodUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StreamHistory");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.StreamGame", b =>
                {
                    b.Property<int>("GameCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StreamId")
                        .HasColumnType("int");

                    b.HasKey("GameCategoryId");

                    b.HasIndex("StreamId");

                    b.ToTable("StreamGame");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.StreamHighlight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HighlightUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HighlighteTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StreamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StreamId");

                    b.ToTable("StreamHighlights");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.CommandAndResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Auth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasLogic")
                        .HasColumnType("bit");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CommandAndResponse");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.Emote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMute")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Sound")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("TimesUsed")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Video")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Emotes");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.SpecialWords", b =>
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

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SpecialWords");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.Target", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Chance")
                        .HasColumnType("int");

                    b.Property<bool>("IsSameTime")
                        .HasColumnType("bit");

                    b.Property<string>("TargetCondition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TargetDataId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TriggerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TriggerId");

                    b.ToTable("Target");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.TargetData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("EmoteId")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("TargetId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmoteId");

                    b.HasIndex("TargetId")
                        .IsUnique();

                    b.ToTable("TargetData");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.Trigger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Ammount")
                        .HasColumnType("int");

                    b.Property<bool>("AmmountCloser")
                        .HasColumnType("bit");

                    b.Property<string>("Auth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ExactAmmount")
                        .HasColumnType("bit");

                    b.Property<string>("TriggerCondition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Trigger");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.Achievements", b =>
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

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.Ban", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BanedAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BanedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ExcludePole")
                        .HasColumnType("bit");

                    b.Property<string>("ExcludeReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBaned")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExcludeChat")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExcludeQueue")
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

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FallowDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FirstChatDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMod")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRaider")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVIP")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<int>("MyProperty")
                        .HasColumnType("int");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TwitchSubId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TwitchSubId")
                        .IsUnique();

                    b.ToTable("Status");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.Sub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CurrentTier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CurrentySubscribed")
                        .HasColumnType("bit");

                    b.Property<bool>("SubGiffted")
                        .HasColumnType("bit");

                    b.Property<int>("SubscribedTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sub");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.User", b =>
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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BanId")
                        .IsUnique();

                    b.HasIndex("StatusId")
                        .IsUnique();

                    b.HasIndex("TwitchAchievementsId")
                        .IsUnique();

                    b.HasIndex("TwitchDetailId")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AppAuthEnum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.Choice", b =>
                {
                    b.HasOne("StreamingApp.Domain.Entities.Internal.Stream.Pole", "Poles")
                        .WithMany("Choices")
                        .HasForeignKey("PoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Poles");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.StreamGame", b =>
                {
                    b.HasOne("StreamingApp.Domain.Entities.Internal.Stream.GameInfo", "GameCategory")
                        .WithMany("GameCategories")
                        .HasForeignKey("GameCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreamingApp.Domain.Entities.Internal.Stream.Stream", "Stream")
                        .WithMany("GameCategories")
                        .HasForeignKey("StreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameCategory");

                    b.Navigation("Stream");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.StreamHighlight", b =>
                {
                    b.HasOne("StreamingApp.Domain.Entities.Internal.Stream.Stream", "Stream")
                        .WithMany("StreamHighlights")
                        .HasForeignKey("StreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stream");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.Target", b =>
                {
                    b.HasOne("StreamingApp.Domain.Entities.Internal.Trigger.Trigger", "Trigger")
                        .WithMany("Targets")
                        .HasForeignKey("TriggerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.TargetData", b =>
                {
                    b.HasOne("StreamingApp.Domain.Entities.Internal.Trigger.Emote", "Emote")
                        .WithMany("TargetData")
                        .HasForeignKey("EmoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreamingApp.Domain.Entities.Internal.Trigger.Target", "Target")
                        .WithOne("TargetData")
                        .HasForeignKey("StreamingApp.Domain.Entities.Internal.Trigger.TargetData", "TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Emote");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.Status", b =>
                {
                    b.HasOne("StreamingApp.Domain.Entities.Internal.User.Sub", "TwitchSub")
                        .WithOne("Status")
                        .HasForeignKey("StreamingApp.Domain.Entities.Internal.User.Status", "TwitchSubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TwitchSub");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.User", b =>
                {
                    b.HasOne("StreamingApp.Domain.Entities.Internal.User.Ban", "Ban")
                        .WithOne("User")
                        .HasForeignKey("StreamingApp.Domain.Entities.Internal.User.User", "BanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreamingApp.Domain.Entities.Internal.User.Status", "Status")
                        .WithOne("User")
                        .HasForeignKey("StreamingApp.Domain.Entities.Internal.User.User", "StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreamingApp.Domain.Entities.Internal.User.Achievements", "TwitchAchievements")
                        .WithOne("User")
                        .HasForeignKey("StreamingApp.Domain.Entities.Internal.User.User", "TwitchAchievementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreamingApp.Domain.Entities.Internal.User.UserDetail", "TwitchDetail")
                        .WithOne("User")
                        .HasForeignKey("StreamingApp.Domain.Entities.Internal.User.User", "TwitchDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ban");

                    b.Navigation("Status");

                    b.Navigation("TwitchAchievements");

                    b.Navigation("TwitchDetail");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.GameInfo", b =>
                {
                    b.Navigation("GameCategories");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.Pole", b =>
                {
                    b.Navigation("Choices");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Stream.Stream", b =>
                {
                    b.Navigation("GameCategories");

                    b.Navigation("StreamHighlights");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.Emote", b =>
                {
                    b.Navigation("TargetData");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.Target", b =>
                {
                    b.Navigation("TargetData")
                        .IsRequired();
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.Trigger.Trigger", b =>
                {
                    b.Navigation("Targets");
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.Achievements", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.Ban", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.Status", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.Sub", b =>
                {
                    b.Navigation("Status")
                        .IsRequired();
                });

            modelBuilder.Entity("StreamingApp.Domain.Entities.Internal.User.UserDetail", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
