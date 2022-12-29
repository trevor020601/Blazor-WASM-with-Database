using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using p6_c00445623_c00441253.Shared;
namespace p6_c00445623_c00441253.Server.Controllers;
[ApiController]
[Route("[controller]")]
public class DictionaryController : ControllerBase
{
    private readonly ILogger<DictionaryController> _logger;
    public DictionaryController(ILogger<DictionaryController> logger)
    {
        _logger = logger;
    }
    
    [HttpPut]
    public void Put(Dictionary word)
    {
        if (word != null) {
            using (var db = new DictionaryDbContext())
            {
                try
                {
                    db.Words.Add(word); // auto-increment Id
                    db.SaveChanges();
                }
                catch
                {
                    Console.WriteLine($"Adding {word.Word} failed\n");
                }
            }
        }
        else
            Console.WriteLine("Contact was null");
    }
    [HttpGet]
    public Dictionary[] Get()
    {
        List<Dictionary> list = new List<Dictionary>();

        using (var db = new DictionaryDbContext())
        {
            var contactList =
                from x in db.Words
                orderby x.Word
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
            using var db = new DictionaryDbContext();
            var thingie = db.Words.FirstOrDefault(_ => _.Id == id);
            if (thingie != null)
            {
                db.Entry(thingie).State = EntityState.Modified;
                db.Words.Remove(thingie);
                db.SaveChanges();
            }
        }
        catch
        {
        }
    }
}

