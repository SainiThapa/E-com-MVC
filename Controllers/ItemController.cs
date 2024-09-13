using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EcomMVC.Models;
using EcomMVC.ViewModel;
using EcomMVC.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EcomMVC.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ItemController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemController(UserManager<User> userManager, ILogger<ItemController> logger,ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            var items = _context.Items.ToList();

            if (roles.Contains("Admin"))
            {
                return View("AdminItemList", items); // For Admin
            }
            else if (roles.Contains("User"))
            {
                return View("ItemList", items); // For User
            }

            return Unauthorized();
        }

        // Admin: Add new item
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddItem(ItemViewModel model, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile == null || ImageFile.Length == 0)
                    {
                        ModelState.AddModelError("ImageFile", "Please upload an image.");
                        return View(model);
                    }
                // Save the image to a folder on the server
                string imagePath = await SaveImageFileAsync(ImageFile);

                // var sellerId = _userManager.GetUserId(User);

                var item = new Item
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    ImagePath = imagePath,
                    // SellerId = sellerId // Assign the current admin's ID as the seller

                };

                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError("Validation error: " + error.ErrorMessage);
                    }
                }
            }
            return View(model);
        }

        // Admin: Edit Item
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            var model = new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity,
                ImagePath = item.ImagePath
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditItem(ItemViewModel model, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                var item = _context.Items.Find(model.Id);
                if (item == null) return NotFound();


                // If a new image is uploaded, save it
                if (ImageFile != null)
                {
                    string imagePath = await SaveImageFileAsync(ImageFile);
                    item.ImagePath = imagePath;
                }


                item.Name = model.Name;
                item.Description = model.Description;
                item.Price = model.Price;
                item.Quantity = model.Quantity;
                item.ImagePath = model.ImagePath;

                _context.Items.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        private async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                return "/images/" + uniqueFileName; // Return the relative image path
            }
            return null;
        }
        // Admin: Delete Item
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // User: View Item Details
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            var model = new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity,
                ImagePath = item.ImagePath
            };

            return View(model);
        }

// Search Functionality
        [HttpPost]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                // Return an empty list if the search query is empty
                return View(new List<Item>());
            }

            // Search items by name (case-insensitive)
            var items = _context.Items
                                .Where(i => i.Name.ToLower().Contains(query.ToLower()))
                                .ToList();
            return View(items);
        }
    }
}
