using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Certificates
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Certificate> Certificates { get; set; } = new();

        public async Task OnGetAsync()
        {
            Certificates = await _context.Certificates
                .OrderByDescending(c => c.Date)
                .ToListAsync();
        }
    }
}