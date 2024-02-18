using var db = new BloggingContext();

// Create
Console.WriteLine("Inserting a new blog");
Blog firstBlog = new Blog
{
    BlogId = Guid.NewGuid().ToString(),
    Url = "http://blogs.msdn.com/adonet"
};

db.Add(firstBlog);
db.SaveChanges();

// Read
Console.WriteLine("Querying for a blog");
var blog = db.Blogs
    .OrderBy(b => b.BlogId)
    .First();

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
Post post = new Post
{
    Title = "Hello World",
    Content = "I wrote an app using EF Core!",
    PostId = Guid.NewGuid().ToString(),
};
blog.Posts!.Add(post);
db.SaveChanges();

// Delete
Console.WriteLine("Delete the blog");
db.Remove(blog);
db.SaveChanges();