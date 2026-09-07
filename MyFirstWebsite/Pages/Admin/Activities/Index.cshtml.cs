using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Activities
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Activity> Activities { get; set; } = new();

        public async Task OnGetAsync()
        {
            Activities = await _context.Activities
                .OrderByDescending(a => a.StartDate)
                .ToListAsync();
        }
    }
}