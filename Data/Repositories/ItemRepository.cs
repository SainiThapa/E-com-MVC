using EcomMVC.Models;
using EcomMVC.Data;
public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _context;

    public ItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Item GetItemById(int id)
    {
        return _context.Items.Find(id);
    }
}
