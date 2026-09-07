using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Activities
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Activity Activity { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(int id)
        {
            var activity = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null)
            {
                return NotFound();
            }

            Activity = activity;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var activity = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == Activity.Id);

            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}