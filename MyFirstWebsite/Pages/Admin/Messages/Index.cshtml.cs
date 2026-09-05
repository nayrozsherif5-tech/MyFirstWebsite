using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Messages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ContactMessage> Messages { get; set; } = new();

        public async Task OnGetAsync()
        {
            Messages = await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }


        // ?? DELETE MESSAGE
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            _context.ContactMessages.Remove(message);

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        // READ / UNREAD
        public async Task<IActionResult> OnPostToggleReadAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            message.IsRead = !message.IsRead;

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

    }
}