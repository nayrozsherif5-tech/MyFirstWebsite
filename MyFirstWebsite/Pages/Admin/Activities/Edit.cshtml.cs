using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Activities
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var activityInDb = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == Activity.Id);

            if (activityInDb == null)
            {
                return NotFound();
            }

            activityInDb.Title = Activity.Title;
            activityInDb.Organization = Activity.Organization;
            activityInDb.Description = Activity.Description;
            activityInDb.StartDate = Activity.StartDate;
            activityInDb.EndDate = Activity.EndDate;
            activityInDb.Link = Activity.Link;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}