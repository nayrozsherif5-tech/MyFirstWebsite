using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Certificates
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DeleteModel(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Certificate Certificate { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);

            if (certificate == null)
            {
                return NotFound();
            }

            Certificate = certificate;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);

            if (certificate == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(certificate.ImagePath))
            {
                string relativePath =
                    certificate.ImagePath.TrimStart('/')
                        .Replace('/', Path.DirectorySeparatorChar);

                string filePath =
                    Path.Combine(_environment.WebRootPath, relativePath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Certificates.Remove(certificate);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Certificates/Index");
        }
    }
}