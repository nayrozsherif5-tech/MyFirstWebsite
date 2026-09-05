using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Skills
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Skill> Skills { get; set; } = new();

        public async Task OnGetAsync()
        {
            Skills = await _context.Skills
                .OrderBy(s => s.Category)
                .ThenBy(s => s.Name)
                .ToListAsync();
        }
    }
}