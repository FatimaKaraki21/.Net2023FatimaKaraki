using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Infrastructure.Components
{
    public class CategoriesViewComponent : ViewComponent
    {

        private readonly DataContext _context;

        public CategoriesViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Categories.ToListAsync());

        private Task<List<Category>> GetCategoriesAsync()
        {
            return _context.Categories.OrderBy(x => x.Sorting).ToListAsync();
        }
    }
}
