
using HC.Domain.Story;
using HC.Domain.Story.Comment;
using HC.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace HC.Infrastructure.DataAccess;

public class HiscaryContext : DbContext
{
    public HiscaryContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<StoryBookMark> StoryBookMarks { get; set; }
    public DbSet<StoryPage> StoryPages { get; set; }
    public DbSet<StoryReadHistory> StoryReadHistories { get; set; }
    public DbSet<StoryRating> StoryScores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}