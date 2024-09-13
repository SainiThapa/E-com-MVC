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
        public void Add(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
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
        
        public int ClearCart(Guid cartId)
        {
            var cartItems = _context.CartItems.Where(x => x.CartId == cartId).ToList();
            if (cartItems != null && cartItems.Any())
            {
                _context.CartItems.RemoveRange(cartItems);
                return _context.SaveChanges();
            }
            return 0;
        }

        // public Cart GetCartById(Guid cartId)
        // {
        //     var cart = _context.Carts.Include("Items").
        //     Where(x.Id == x.cartId && x.isActive == true).FirstOrDefault();
        //     return cart;
            
        // }

        public Cart GetCartByUserId(string userId)
        {
            return _context.Carts.FirstOrDefault(c => c.BuyerId == userId && c.isActive);
        }
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

        public int UpdateCart(Guid cartId, string userId)
        {
            Cart cartfromDb = GetCartById(cartId);
            cartfromDb.BuyerId=userId;
            return _context.SaveChanges();
        }

        // public int UpdateQty(Guid cartId, int itemId, int Quantity)
        // {
        //     bool flag = false;
        //     var cartfromDb = GetCartById(cartId);
        //     if (cartfromDb!=null)
        //     {
        //         for (int i=0; i<cartfromDb.Items.Count; i++)
        //         {
        //             if (cartfromDb.Items[i].Id ==itemId)
        //             {
        //                 flag = true;
        //                 if (Quantity>0)
        //                     cartfromDb.Items[i].Quantity +=(Quantity);
        //                 break;
        //             }
        //         }
        //         if (flag){
        //             return _context.SaveChanges();
        //         }
        //     }
        //     return 0;
        // } 

        public int UpdateQty(Guid cartId, int itemId, int quantity)
        {
            bool itemExists = false;
            var cart = GetCartById(cartId);

            if (cart != null)
            {
                _context.Attach(cart);

                // Check if the item already exists in the cart
                foreach (var cartItem in cart.Items)
                {
                    if (cartItem.ItemId == itemId)
                    {
                        itemExists = true;
                        if(quantity>0)
                        {

                        cartItem.Quantity += quantity; // Update quantity if item exists
                        _context.Entry(cartItem).State = EntityState.Modified; // Mark as modified
                        }
                        break;
                    }
                }

                // If the item doesn't exist, create a new CartItem and add it to the cart
                if (!itemExists)
                {
                    var item = _context.Items.FirstOrDefault(i => i.Id == itemId); // Find the item in the items table
                    if (item != null)
                    {
                        CartItem newCartItem = new CartItem
                        {
                            ItemId = item.Id,
                            Quantity = quantity,
                            Price = item.Price,
                            Name = item.Name,
                            CartId = cartId
                        };
                        cart.Items.Add(newCartItem);
                        _context.CartItems.Add(newCartItem); // Ensure the new CartItem is tracked by EF
                    
                    }
                }

                return _context.SaveChanges(); // Save the changes to the database
            }

            return 0;
        }

    }   
}