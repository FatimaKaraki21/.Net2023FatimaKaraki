using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;
using System.Security.Claims;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string categorySlug = "", int p = 1, string sortOrder = "", string search = "")
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.CategorySlug = categorySlug;

            var productsQuery = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(categorySlug))
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == categorySlug);
                if (category == null)
                {
                    return RedirectToAction("Index");
                }


                productsQuery = productsQuery.Where(p => p.CategoryId == category.Id);
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "price_asc":
                        productsQuery = productsQuery.OrderBy(p => p.Price);
                        break;
                    case "price_desc":
                        productsQuery = productsQuery.OrderByDescending(p => p.Price);
                        break;
                    default:

                        productsQuery = productsQuery.OrderByDescending(p => p.Id);
                        break;
                }
            }

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsQuery.Count() / pageSize);
            if (!string.IsNullOrEmpty(search))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }
            return View(await productsQuery.Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
        }

        [Route("/products/details")]
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            return View(product);
        }


        [Route("/products/Rate")]
        public async Task<IActionResult> Rate(Product product)
        {
            var temp = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            var oldRating = temp.Rating;

            if (oldRating == 0)
            {
                temp.Rating = product.Rating;
            }
            else
            {
                var newRating = (oldRating + product.Rating) / 2;

                temp.Rating = newRating;
            }

            temp.RatingCount += 1;


            _context.Update(temp);
            var res = await _context.SaveChangesAsync() > 0;

            if (res)
            {
                TempData["Success"] = "Rating Submitted!";
                return RedirectToAction("Index", "Products");
            }

            return BadRequest("Bad");
        }

        [Route("/products/Review")]
        public async Task<IActionResult> Review(Product product)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var productId = product.Id;

            var body = product.body;

            await _context.Reviews.AddAsync(new Review()
            {
                UserId = userId,
                ProductId = productId,
                body = body
            });

            var res = await _context.SaveChangesAsync() > 0;

            if (res)
            {
                TempData["Success"] = "The Review has been submitted!";
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ReadReviews(long id)
        {
            var reviews = await _context.Reviews
                .Include(r => r.AppUser).Where(r => r.ProductId == id)
                .ToListAsync();

            return View(reviews);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(long id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                TempData["Error"] = "The review does not exist";
            }
            else
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The review has been deleted";
            }

            return RedirectToAction("Index");
        }



    }

}

