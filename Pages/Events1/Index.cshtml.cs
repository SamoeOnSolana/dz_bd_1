using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App1.Data1;
using App1.Models1;

namespace App1.Pages.Events1;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Event> Events { get; set; } = new List<Event>();
    
    [BindProperty(SupportsGet = true)]
    public string? SearchQuery { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? SortBy { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;
    
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Events
            .Include(e => e.EventParticipants)
                .ThenInclude(ep => ep.Participant)
            .AsQueryable();

        if (!string.IsNullOrEmpty(SearchQuery))
        {
            query = query.Where(e => e.Title.Contains(SearchQuery) || e.Location.Contains(SearchQuery));
        }

        if (SortBy == "participants")
        {
            query = query.OrderByDescending(e => e.EventParticipants.Count);
        }
        else if (SortBy == "participantsdesc")
        {
            query = query.OrderBy(e => e.EventParticipants.Count);
        }

        TotalCount = await query.CountAsync();
        TotalPages = (int)Math.Ceiling(TotalCount / 10.0);

        Events = await query
            .Skip((PageNumber - 1) * 10)
            .Take(10)
            .ToListAsync();
    }
}
