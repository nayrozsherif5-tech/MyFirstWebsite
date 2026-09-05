using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Skills
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Skill Skill { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Skills.Add(Skill);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Skills/Index");
        }

        public async Task<IActionResult> OnPostAddDefaultSkillsAsync()
        {
            var skills = new List<Skill>
            {
                new Skill { Name = "C", Category = "Programming Languages" },
                new Skill { Name = "C++", Category = "Programming Languages" },
                new Skill { Name = "C#", Category = "Programming Languages" },
                new Skill { Name = "Python", Category = "Programming Languages" },

                new Skill { Name = "HTML", Category = "Web Development" },
                new Skill { Name = "CSS", Category = "Web Development" },
                new Skill { Name = "JavaScript", Category = "Web Development" },
                new Skill { Name = "ASP.NET", Category = "Web Development" },

                new Skill { Name = "MATLAB", Category = "Engineering Tools" },
                new Skill { Name = "Fusion 360", Category = "Engineering Tools" },
                new Skill { Name = "AutoCAD", Category = "Engineering Tools" },
                new Skill { Name = "Blender", Category = "Engineering Tools" }
            };

            foreach (var skill in skills)
            {
                bool exists = _context.Skills.Any(s => s.Name == skill.Name);

                if (!exists)
                {
                    _context.Skills.Add(skill);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Skills/Index");
        }
    }
}