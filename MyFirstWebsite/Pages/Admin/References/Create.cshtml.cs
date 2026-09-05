using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.References
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Reference Reference { get; set; } = new();

        [BindProperty]
        public IFormFile? ReferenceFile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ReferenceFile != null && ReferenceFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "References");

                Directory.CreateDirectory(uploadsFolder);

                string extension = Path.GetExtension(ReferenceFile.FileName);

                string fileName =
                    Guid.NewGuid().ToString() + extension;

                string filePath =
                    Path.Combine(uploadsFolder, fileName);

                using (var stream =
                    new FileStream(filePath, FileMode.Create))
                {
                    await ReferenceFile.CopyToAsync(stream);
                }

                Reference.FilePath =
                    "/References/" + fileName;
            }

            _context.References.Add(Reference);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/References/Index");
        }
    }
}