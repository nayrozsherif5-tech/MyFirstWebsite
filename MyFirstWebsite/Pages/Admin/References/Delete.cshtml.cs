using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.References
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reference Reference { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var reference = await _context.References.FindAsync(id);

            if (reference == null)
            {
                return NotFound();
            }

            Reference = reference;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var reference = await _context.References.FindAsync(Reference.Id);

            if (reference == null)
            {
                return NotFound();
            }

            _context.References.Remove(reference);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/References/Index");
        }
    }
}