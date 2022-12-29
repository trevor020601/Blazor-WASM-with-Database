using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using p6_c00445623_c00441253.Shared;
namespace p6_c00445623_c00441253.Server.Controllers;
[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;
    public ContactController(ILogger<ContactController> logger)
    {
        _logger = logger;
    }
    
    [HttpPut]
    public void Put(Contact contact)
    {
        if (contact != null) {
            using (var db = new ContactsDbContext())
            {
                try
                {
                    db.Contacts.Add(contact); // auto-increment Id
                    db.SaveChanges();
                }
                catch
                {
                    Console.WriteLine($"Adding {contact.Name} failed\n");
                }
            }
        }
        else
            Console.WriteLine("Contact was null");
    }
    [HttpGet]
    public Contact[] Get()
    {
        List<Contact> list = new List<Contact>();

        using (var db = new ContactsDbContext())
        {
            var contactList =
                from x in db.Contacts
                orderby x.Name
                select x;
            foreach (var c in contactList)
                list.Add(c);
        }

        return list.ToArray();
    }
    
    [HttpDelete("{Id}")]
    public void Delete(string Id)
    {
        try
        {
            int id = Int32.Parse(Id);
            using var db = new ContactsDbContext();
            var thingie = db.Contacts.FirstOrDefault(_ => _.Id == id);
            if (thingie != null)
            {
                db.Entry(thingie).State = EntityState.Modified;
                db.Contacts.Remove(thingie);
                db.SaveChanges();
            }
        }
        catch { }
    }
}