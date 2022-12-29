using Microsoft.EntityFrameworkCore;
using p6_c00445623_c00441253.Shared;
namespace p6_c00445623_c00441253.Server;
public class ContactsDbContext: DbContext
{
    protected override void OnConfiguring(
        DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=contacts.db");
    public DbSet<Contact>? Contacts { get; set; }
}