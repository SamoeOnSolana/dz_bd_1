using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App1.Data1;
using App1.Models1;

namespace App1.Pages.Orders1;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Order> Orders { get; set; } = new List<Order>();
    
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
        var query = _context.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .AsQueryable();

        if (!string.IsNullOrEmpty(SearchName))
        {
            query = query.Where(o => o.CustomerName.Contains(SearchName));
        }

        if (SortBy == "date")
        {
            query = query.OrderBy(o => o.OrderDate);
        }
        else if (SortBy == "datedesc")
        {
            query = query.OrderByDescending(o => o.OrderDate);
        }

        TotalCount = await query.CountAsync();
        TotalPages = (int)Math.Ceiling(TotalCount / 5.0);

        Orders = await query
            .Skip((PageNumber - 1) * 5)
            .Take(5)
            .ToListAsync();
    }
}
