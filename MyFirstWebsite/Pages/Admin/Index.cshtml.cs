using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;

namespace MyFirstWebsite.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int UnreadMessagesCount { get; set; }

        public async Task OnGetAsync()
        {
            UnreadMessagesCount = await _context.ContactMessages
                .CountAsync(m => !m.IsRead);
        }
    }
}