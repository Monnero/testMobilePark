using Microsoft.EntityFrameworkCore;

public class NewsDb : DbContext
{
    public NewsDb(DbContextOptions<NewsDb> options) : base(options) { }

    public DbSet<News> News => Set<News>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=news.db");
    }
}
