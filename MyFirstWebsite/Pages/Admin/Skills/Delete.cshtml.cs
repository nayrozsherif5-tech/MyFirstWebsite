using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Skills
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
        public Skill Skill { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            Skill = skill;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var skill = await _context.Skills.FindAsync(Skill.Id);

            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Skills/Index");
        }
    }
}