using Microsoft.EntityFrameworkCore;
using EcomMVC.Data;
using EcomMVC.Data.Infrastructure;
using EcomMVC.Models;
using EcomMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomMVC.Data.Repositories
{
    public class CartRepository: Repository<Cart>, ICartRepository
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

        // public Cart GetCartById(Guid cartId)
        // {
        //     var cart = _context.Carts.Include("Items").
        //     Where(x.Id == x.cartId && x.isActive == true).FirstOrDefault();
        //     return cart;
            
        // }
        public Cart GetCartById(Guid cartId)
        {
            var cart = _context.Carts
                .Include(c => c.Items)
                .FirstOrDefault(x => x.Id == cartId && x.isActive);
            return cart;
        }



        public CartViewModel GetCartDetails(Guid cartId)
        {
            var cartViewModel =  (from cartfromdb in _context.Carts
                                where cartfromdb.Id==cartId && cartfromdb.isActive==true
                                select new CartViewModel
                                {
                                    Id = cartfromdb.Id,
                                    UserId = cartfromdb.BuyerId,
                                    CreatedDate = cartfromdb.CreatedDate,
                                    Items = (from cartItem in _context.CartItems
                                                join item in _context.Items
                                                on cartItem.ItemId equals item.Id
                                                where cartItem.CartId == cartfromdb.Id
                                                select new ItemViewModel
                                                {
                                                    Id = cartItem.Id,
                                                    Name = item.Name,
                                                    Description = item.Description,
                                                    ImagePath = item.ImagePath,
                                                    Quantity = cartItem.Quantity,
                                                    ItemId = item.Id,
                                                    Price = cartItem.Price
                                                }).ToList() 
                                                }).FirstOrDefault();

            return cartViewModel;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            Cart cartfromDb = GetCartById(cartId);
            cartfromDb.BuyerId=userId;
            return _context.SaveChanges();
        }

        public int UpdateQty(Guid cartId, int itemId, int Quantity)
        {
            bool flag = false;
            var cartfromDb = GetCartById(cartId);
            if (cartfromDb!=null)
            {
                for (int i=0; i<cartfromDb.Items.Count; i++)
                {
                    if (cartfromDb.Items[i].Id ==itemId)
                    {
                        flag = true;
                        if (Quantity>0)
                            cartfromDb.Items[i].Quantity +=(Quantity);
                        break;
                    }
                }
                if (flag){
                    return _context.SaveChanges();
                }
            }
            return 0;
        } 
    }   
}