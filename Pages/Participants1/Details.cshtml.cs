using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App1.Data1;
using App1.Models1;

namespace App1.Pages.Participants1;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Participant Participant { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var participant = await _context.Participants
            .Include(p => p.EventParticipants)
                .ThenInclude(ep => ep.Event)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (participant == null)
        {
            return NotFound();
        }
        else
        {
            Participant = participant;
        }
        return Page();
    }
}
