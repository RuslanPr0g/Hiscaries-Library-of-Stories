﻿// <auto-generated />
using System;
using HC.Domain.Users;
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
    [Migration("20241106144320_AddAuditFields")]
    partial class AddAuditFields
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

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("HC.Domain.Stories.Genre", b =>
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

                    b.Property<Guid>("PublisherId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

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

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.HasIndex("UserId");

                    b.ToTable("StoryRating");
                });

            modelBuilder.Entity("HC.Domain.Users.RefreshToken", b =>
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

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("HC.Domain.Users.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PublisherId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReviewerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("HC.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("Banned")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RefreshTokenId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RefreshTokenId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HC.Domain.Users.UserReadHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LastPageRead")
                        .HasColumnType("integer");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.HasIndex("UserId");

                    b.ToTable("ReadHistory");
                });

            modelBuilder.Entity("HC.Domain.Users.UserStoryBookMark", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserStoryBookMark");
                });

            modelBuilder.Entity("GenreStory", b =>
                {
                    b.HasOne("HC.Domain.Stories.Genre", null)
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

            modelBuilder.Entity("HC.Domain.Stories.Comment", b =>
                {
                    b.HasOne("HC.Domain.Stories.Story", "Story")
                        .WithMany("Comments")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HC.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Story");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HC.Domain.Stories.Story", b =>
                {
                    b.HasOne("HC.Domain.Users.User", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Publisher");
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
                    b.HasOne("HC.Domain.Stories.Story", null)
                        .WithMany("Ratings")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HC.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HC.Domain.Users.Review", b =>
                {
                    b.HasOne("HC.Domain.Users.User", "Publisher")
                        .WithMany("Reviews")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HC.Domain.Users.User", "Reviewer")
                        .WithMany()
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Publisher");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("HC.Domain.Users.User", b =>
                {
                    b.HasOne("HC.Domain.Users.RefreshToken", "RefreshToken")
                        .WithOne()
                        .HasForeignKey("HC.Domain.Users.User", "RefreshTokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("HC.Domain.Users.UserReadHistory", b =>
                {
                    b.HasOne("HC.Domain.Stories.Story", "Story")
                        .WithMany()
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HC.Domain.Users.User", null)
                        .WithMany("ReadHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Story");
                });

            modelBuilder.Entity("HC.Domain.Users.UserStoryBookMark", b =>
                {
                    b.HasOne("HC.Domain.Users.User", null)
                        .WithMany("BookMarks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HC.Domain.Stories.Story", b =>
                {
                    b.Navigation("Audios");

                    b.Navigation("Comments");

                    b.Navigation("Contents");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("HC.Domain.Users.User", b =>
                {
                    b.Navigation("BookMarks");

                    b.Navigation("ReadHistory");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
