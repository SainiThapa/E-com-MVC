using EcomMVC.Data.Repositories;
using EcomMVC.Models;
using EcomMVC.ViewModel;
using Rotativa.AspNetCore; // Add this at the top of your controller
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using EcomMVC.Data.Infrastructure;

namespace EcomMVC.Controllers
{
    [Authorize(Roles = "User")]
    public class PurchaseController : Controller
    {

        private readonly IItemRepository _itemRepository;
        private readonly UserManager<User> _userManager;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public PurchaseController(UserManager<User> userManager, IOrderRepository orderRepository, ICartRepository cartRepository, IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _userManager = userManager;
            _itemRepository = itemRepository;
        }

        // Add to Cart Action
        // [HttpPost]
        // public async Task<IActionResult> AddToCart(int itemId, int quantity)
        // {
        //     var cartId = Guid.NewGuid();  // Replace with actual cart ID retrieval logic
        //     var cart = _cartRepository.GetCartById(cartId);

        //     if (cart == null)
        //     {
        //         // Create a new cart for the user if none exists
        //         cart = new Cart { Id = cartId, BuyerId = 1, CreatedDate = DateTime.Now, isActive = true };  // Replace with actual user ID
        //         _cartRepository.Add(cart);
        //     }

        //     // Add the item to the cart
        //     _cartRepository.UpdateQty(cart.Id, itemId, quantity);
        //     return RedirectToAction("CartDetails", new { cartId = cart.Id });
        // }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int itemId, int quantity)
        {
            // Retrieve or create the user's cart
            var userId = _userManager.GetUserId(User); // Get the current user ID
            var cart = _cartRepository.GetCartByUserId(userId);

            if (cart == null)
            {
                cart = new Cart { BuyerId = userId, CreatedDate = DateTime.Now, isActive = true };
                _cartRepository.Add(cart);
            }

            // Add the item to the cart with the specified quantity
            _cartRepository.UpdateQty(cart.Id, itemId, quantity);
            // return RedirectToAction("Cart");
            return RedirectToAction("CartDetails", new { cartId = cart.Id });
        }


        // View Cart Details
        [HttpGet]
        public IActionResult CartDetails(Guid cartId)
        {
            var cart = _cartRepository.GetCartDetails(cartId);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }


        // public IActionResult Cart()
        // {
        //     var userId = _userManager.GetUserId(User); // Get the current logged-in user's ID
        //     var cart =  _cartRepository.GetCartByUserId(userId);

        //     if(cart==null)
        //     {
        //         ViewBag.Message = "Your Cart is Empty";
        //         return View(new CartViewModel());
        //     }
        //     return View(cart);
        // }

        // [HttpPost]
        // public async Task<IActionResult> Buy(int itemId, int quantity)
        // {
        //     // Process the purchase (you can define how to handle orders and payments)
        //     var item = _itemRepository.GetItemById(itemId);
        //     if (item == null || item.Quantity < quantity)
        //     {
        //         return BadRequest("Item not available in requested quantity");
        //     }

        //     return RedirectToAction("PaymentDetails", new { itemId = item.Id, quantity = quantity });
        // }

        [HttpPost]
        public IActionResult RemoveFromCart(int itemId)
        {
            var userId = _userManager.GetUserId(User); // Get the current logged-in user's ID
            var cart = _cartRepository.GetCartByUserId(userId);

            if (cart != null)
            {
                _cartRepository.Delete(cart.Id, itemId); // Delete the item from the cart
            }

            return RedirectToAction("CartDetails",new { cartId = cart.Id });
        }


        // Checkout Action
        public IActionResult Checkout(Guid cartId)
        {
            var cart = _cartRepository.GetCartDetails(cartId);
            if (cart == null)
            {
                return NotFound();
            }

            // Proceed to payment (this is where you would integrate payment functionality)
            return View(cart);
        }

        // Payment Details Action
        [HttpPost]
        public IActionResult PaymentDetails(Guid cartId)
        {
            var cart = _cartRepository.GetCartDetails(cartId);
            if (cart == null)
            {
                return NotFound();
            }

            // Create a new order from the cart
            var order = new Order
            {
                Id=Guid.NewGuid().ToString(),
                UserId = cart.UserId,
                PaymentId = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now,
                OrderItems = cart.Items.Select(i => new OrderItem
                {
                    ItemId = i.ItemId,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    Total = i.Price * i.Quantity
                }).ToList()
            };      
            _orderRepository.Add(order);

            _cartRepository.ClearCart(cartId); // Custom method to remove all items in the cart

            return RedirectToAction("OrderDetails", new { orderId = order.Id });
        }
        // Order Details Action
        public IActionResult OrderDetails(string orderId)
        {
            var orderDetails = _orderRepository.GetOrderDetails(orderId);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        [HttpPost]
        public IActionResult DownloadPdf(string orderId)
        {
            var orderDetails = _orderRepository.GetOrderDetails(orderId);
            if (orderDetails == null)
            {
                return NotFound();
            }
            return new ViewAsPdf("OrderDetails", orderDetails) // Use Rotativa to convert the view to PDF
            {
                FileName = $"Order_{orderId}.pdf"
            };
        }
    }
}
