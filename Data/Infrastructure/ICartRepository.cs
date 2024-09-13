using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcomMVC.Models;
using EcomMVC.ViewModel;


namespace EcomMVC.Data.Infrastructure
{
    public interface ICartRepository: IRepository<Cart>
    {
        Cart GetCartById(Guid id);
        CartViewModel GetCartDetails (Guid CartId);
        void Add(Cart cart);
        int ClearCart(Guid cartId);
        int Delete (Guid cartId, int itemId);
        int UpdateQty (Guid cartId, int itemId, int Quanity);
        int UpdateCart (Guid cartId, string userId);
        Cart GetCartByUserId(string userId);


    }
}