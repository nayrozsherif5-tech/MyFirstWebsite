using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Pages.Admin.Certificates
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Certificate Certificate { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Certificate.ImagePath");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingCertificate =
                await _context.Certificates.FindAsync(Certificate.Id);

            if (existingCertificate == null)
            {
                return NotFound();
            }

            existingCertificate.Title = Certificate.Title;
            existingCertificate.Organization = Certificate.Organization;
            existingCertificate.Description = Certificate.Description;
            existingCertificate.Date = Certificate.Date;
            existingCertificate.CertificateLink = Certificate.CertificateLink;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "certificates");

                Directory.CreateDirectory(uploadsFolder);

                string extension = Path.GetExtension(ImageFile.FileName);
                string fileName = Guid.NewGuid().ToString() + extension;

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                existingCertificate.ImagePath =
                    "/certificates/" + fileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Certificates/Index");
        }
    }
}