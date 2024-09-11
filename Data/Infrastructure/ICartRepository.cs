using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomMVC.Data.Infrastructure
{
    public interface ICartRepository: IRepository<Cart>
    {
        Cart GetCartById(int id);
        CartViewModel GetCartDetails (Guid CartId);
        int Delete (Guid cartId, int itemId);
        int UpdateQty (Guid cartId, int itemId, int Quanity);
        int UpdateCart (Guid cartId, int userId);
    }
}