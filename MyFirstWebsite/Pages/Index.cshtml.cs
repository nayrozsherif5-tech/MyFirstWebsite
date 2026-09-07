using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Skills from database
        public List<Skill> Skills { get; set; } = new();

        // Activities from database
        public List<Activity> Activities { get; set; } = new();


        public async Task OnGetAsync()
        {
            /* =====================================================
               SKILLS
            ===================================================== */

            var order = new List<string>
            {
                "C",
                "C++",
                "C#",
                "Python",
                "HTML",
                "CSS",
                "JavaScript",
                "MATLAB",
                "Fusion 360",
                "AutoCAD",
                "Blender",
                "ASP.NET"
            };

            Skills = await _context.Skills.ToListAsync();

            Skills = Skills
                .OrderBy(s =>
                {
                    int index = order.IndexOf(s.Name);

                    // Skills not found in the technical order,
                    // such as Soft Skills, go after technical skills.
                    return index == -1
                        ? int.MaxValue
                        : index;
                })
                .ToList();


            /* =====================================================
               ACTIVITIES
            ===================================================== */

            Activities = await _context.Activities
                .OrderByDescending(a => a.StartDate)
                .ToListAsync();
        }
    }
}