using ShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Infrastructure
{


    public class MainMenuViewComponent : ViewComponent
    {
        private readonly DataContext context;

        public MainMenuViewComponent(DataContext context)
        {
            this.context = context;
        }



        public async Task<IViewComponentResult> InvokeAsync()
        {

            var pages = await GetPagesAsync();


            return View(pages);
        }

        private Task<List<Page>> GetPagesAsync()
        {
            return context.Pages.OrderBy(x => x.Sorting).ToListAsync();
        }
    }
}