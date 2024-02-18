using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    public DbSet<Blog>? Blogs { get; set; }
    public DbSet<Post>? Posts { get; set; }


    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("User ID=postgres;Password=1234;" +
                             "Host=localhost;Port=5432;Database=staracademy;" +
                             "Connection Lifetime=0;");
}

public class Blog
{
    public string BlogId { get; set; }
    public string? Url { get; set; }

    public List<Post>? Posts { get; } = new();
}

public class Post
{
    
    public string PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    public int BlogId { get; set; }
    public Blog? Blog { get; set; }
}