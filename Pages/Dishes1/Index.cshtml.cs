using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App1.Data1;
using App1.Models1;

namespace App1.Pages.Dishes1;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Dish> Dishes { get; set; } = new List<Dish>();
    public IList<Category> Categories { get; set; } = new List<Category>();
    
    [BindProperty(SupportsGet = true)]
    public int? CategoryFilter { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Dishes
            .Include(d => d.Restaurant)
            .Include(d => d.DishCategories)
                .ThenInclude(dc => dc.Category)
            .AsQueryable();

        if (CategoryFilter.HasValue)
        {
            query = query.Where(d => d.DishCategories.Any(dc => dc.CategoryId == CategoryFilter.Value));
        }

        Dishes = await query.ToListAsync();
        Categories = await _context.Categories.ToListAsync();
    }
}
