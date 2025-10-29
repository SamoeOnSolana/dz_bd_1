using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App1.Data1;
using App1.Models1;

namespace App1.Pages.Restaurants1;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    
    [BindProperty(SupportsGet = true)]
    public string? SearchName { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? SortBy { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;
    
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Restaurants
            .Include(r => r.Dishes)
                .ThenInclude(d => d.DishCategories)
                    .ThenInclude(dc => dc.Category)
            .AsQueryable();

        if (!string.IsNullOrEmpty(SearchName))
        {
            query = query.Where(r => r.Name.Contains(SearchName));
        }

        if (SortBy == "price")
        {
            query = query.OrderBy(r => r.Dishes.OrderBy(d => d.Price).FirstOrDefault() != null ? r.Dishes.OrderBy(d => d.Price).FirstOrDefault()!.Price : 0);
        }
        else if (SortBy == "pricedesc")
        {
            query = query.OrderByDescending(r => r.Dishes.OrderByDescending(d => d.Price).FirstOrDefault() != null ? r.Dishes.OrderByDescending(d => d.Price).FirstOrDefault()!.Price : 0);
        }

        TotalCount = await query.CountAsync();
        TotalPages = (int)Math.Ceiling(TotalCount / 5.0);

        Restaurants = await query
            .Skip((PageNumber - 1) * 5)
            .Take(5)
            .ToListAsync();
    }
}
