using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly DataContext context;
        public PagesController(DataContext context) {

            this.context = context;

        }
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in context.Pages orderby p.Sorting select p;
            List<Page> pageslist = await pages.ToListAsync();
            return View(pageslist);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid) {
                page.Slug = page.Title.ToLower().Replace(" ", " ");
                page.Sorting = 100;

                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The page already exists");
                    return View(page);
                }
                context.Add(page);
                await context.SaveChangesAsync();
                TempData["Success"] = "The page has been added";
                return RedirectToAction("Index");
                      
            }
            return View(page);

        }
       
        public async Task<IActionResult> Edit(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Slug = page.Title.ToLower().Replace(" ", " ");

                var slug = await context.Pages.Where(x => x.Id != page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {

                    ModelState.AddModelError("", "The Page already exists.");
                    return View(page);
                }

                context.Update(page);
                await context.SaveChangesAsync();


                TempData["Success"] = "The page has been edited";




                return RedirectToAction("Edit", new { id = page.Id });
            }
            return View(page);
        }

        
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }


        
        public async Task<IActionResult> Delete(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            if (page == null)
            {
                TempData["Error"] = "The page does not exist";
            }
            else
            {
                context.Pages.Remove(page);
                await context.SaveChangesAsync();
                TempData["Success"] = "The page has been deleted";
            }

            return RedirectToAction("Index");
        }




        /*        [HttpPost]
                public async Task<IActionResult> Reorder(int[] id)
                {
                    int count = 1;

                    foreach (var pageID in id)
                    {

                        Page page = await context.Pages.FindAsync(pageID);
                        page.Sorting = count;
                        context.Update(page);
                        await context.SaveChangesAsync();
                        count++;


                    }

                    return Ok();

                }*/

        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            if (id == null || id.Length == 0)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                int count = 1;

                foreach (var pageID in id)
                {
                    Page page = await context.Pages.FindAsync(pageID);

                    if (page != null)
                    {
                        page.Sorting = count;
                        context.Update(page);
                        count++;
                    }
                }

                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




    }
}
