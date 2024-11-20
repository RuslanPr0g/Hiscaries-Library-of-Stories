﻿// <auto-generated />
using System;
using System.Collections.Generic;
using HC.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HC.Persistence.Context.Migrations
{
    [DbContext(typeof(HiscaryContext))]
    [Migration("20241119064943_AddIndex")]
    partial class AddIndex
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GenreStory", b =>
                {
                    b.Property<Guid>("GenresId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.HasKey("GenresId", "StoryId");

                    b.HasIndex("StoryId");

                    b.ToTable("StoryGenres", (string)null);
                });

            modelBuilder.Entity("HC.Application.Common.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<DateTime>("OccuredOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", (string)null);
                });

            modelBuilder.Entity("HC.Domain.Genres.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("ImagePreview")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.Library", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("LinksToSocialMedia")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<Guid>("PlatformUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlatformUserId");

                    b.ToTable("Library");
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.PlatformUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserAccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId")
                        .IsUnique()
                        .HasDatabaseName("IX_UserAccountId");

                    b.ToTable("PlatformUsers");
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.ReadingHistory", b =>
                {
                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlatformUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LastPageRead")
                        .HasColumnType("integer");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("StoryId", "PlatformUserId");

                    b.HasIndex("PlatformUserId");

                    b.ToTable("ReadingHistory");
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.Review", b =>
                {
                    b.Property<Guid>("LibraryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlatformUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("LibraryId", "PlatformUserId");

                    b.HasIndex("PlatformUserId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.StoryBookMark", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PlatformUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlatformUserId");

                    b.ToTable("StoryBookMark");
                });

            modelBuilder.Entity("HC.Domain.Stories.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PlatformUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlatformUserId");

                    b.HasIndex("StoryId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("HC.Domain.Stories.Story", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("AgeLimit")
                        .HasColumnType("integer");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateWritten")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImagePreviewUrl")
                        .HasColumnType("text");

                    b.Property<Guid>("LibraryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LibraryId");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("HC.Domain.Stories.StoryAudio", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.ToTable("StoryAudio");
                });

            modelBuilder.Entity("HC.Domain.Stories.StoryPage", b =>
                {
                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Page")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("StoryId", "Page");

                    b.ToTable("StoryPage");
                });

            modelBuilder.Entity("HC.Domain.Stories.StoryRating", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PlatformUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlatformUserId");

                    b.HasIndex("StoryId");

                    b.ToTable("StoryRating");
                });

            modelBuilder.Entity("HC.Domain.UserAccounts.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Invalidated")
                        .HasColumnType("boolean");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Used")
                        .HasColumnType("boolean");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("HC.Domain.UserAccounts.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RefreshTokenId")
                        .HasColumnType("uuid");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RefreshTokenId")
                        .IsUnique();

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("GenreStory", b =>
                {
                    b.HasOne("HC.Domain.Genres.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HC.Domain.Stories.Story", null)
                        .WithMany()
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.Library", b =>
                {
                    b.HasOne("HC.Domain.PlatformUsers.PlatformUser", "PlatformUser")
                        .WithMany("Libraries")
                        .HasForeignKey("PlatformUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlatformUser");
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.ReadingHistory", b =>
                {
                    b.HasOne("HC.Domain.PlatformUsers.PlatformUser", null)
                        .WithMany("ReadHistory")
                        .HasForeignKey("PlatformUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HC.Domain.Stories.Story", null)
                        .WithMany("ReadHistory")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.Review", b =>
                {
                    b.HasOne("HC.Domain.PlatformUsers.PlatformUser", null)
                        .WithMany("Reviews")
                        .HasForeignKey("PlatformUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.StoryBookMark", b =>
                {
                    b.HasOne("HC.Domain.PlatformUsers.PlatformUser", null)
                        .WithMany("Bookmarks")
                        .HasForeignKey("PlatformUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HC.Domain.Stories.Comment", b =>
                {
                    b.HasOne("HC.Domain.PlatformUsers.PlatformUser", "PlatformUser")
                        .WithMany()
                        .HasForeignKey("PlatformUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HC.Domain.Stories.Story", "Story")
                        .WithMany("Comments")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlatformUser");

                    b.Navigation("Story");
                });

            modelBuilder.Entity("HC.Domain.Stories.Story", b =>
                {
                    b.HasOne("HC.Domain.PlatformUsers.Library", "Library")
                        .WithMany()
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Library");
                });

            modelBuilder.Entity("HC.Domain.Stories.StoryAudio", b =>
                {
                    b.HasOne("HC.Domain.Stories.Story", null)
                        .WithMany("Audios")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HC.Domain.Stories.StoryPage", b =>
                {
                    b.HasOne("HC.Domain.Stories.Story", null)
                        .WithMany("Contents")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HC.Domain.Stories.StoryRating", b =>
                {
                    b.HasOne("HC.Domain.PlatformUsers.PlatformUser", "PlatformUser")
                        .WithMany()
                        .HasForeignKey("PlatformUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HC.Domain.Stories.Story", null)
                        .WithMany("Ratings")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlatformUser");
                });

            modelBuilder.Entity("HC.Domain.UserAccounts.UserAccount", b =>
                {
                    b.HasOne("HC.Domain.UserAccounts.RefreshToken", "RefreshToken")
                        .WithOne()
                        .HasForeignKey("HC.Domain.UserAccounts.UserAccount", "RefreshTokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("HC.Domain.PlatformUsers.PlatformUser", b =>
                {
                    b.Navigation("Bookmarks");

                    b.Navigation("Libraries");

                    b.Navigation("ReadHistory");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("HC.Domain.Stories.Story", b =>
                {
                    b.Navigation("Audios");

                    b.Navigation("Comments");

                    b.Navigation("Contents");

                    b.Navigation("Ratings");

                    b.Navigation("ReadHistory");
                });
#pragma warning restore 612, 618
        }
    }
}