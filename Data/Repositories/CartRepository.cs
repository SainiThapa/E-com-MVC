using EcomMVC.Data;
using EcomMVC.Data.Infrastructure;
using EcomMVC.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomMVC.Data.Repositories
{
    public class CartRepository: CartRepository<Cart>, ICartRepository
    {
        private ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _context = dbContext;
        }
        public int Delete(Guid cartId, int itemId){
            var item = _context.CartItems.Where(x=>x.CartId == cartId && x.Id==itemId).FirstOrDefault();
            if(item!=null)
            {
                _context.CartItems.Remove(item);    
                return _context.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public Cart GetCartById(Guid cartId)
        {
            throw new NotImplementedException();
        }
    }
}