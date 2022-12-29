using Microsoft.EntityFrameworkCore;
using p6_c00445623_c00441253.Shared;
namespace p6_c00445623_c00441253.Server;
public class DictionaryDbContext: DbContext
{
    protected override void OnConfiguring(
        DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=dictionary.db");
    public DbSet<Dictionary>? Words { get; set; }
}