using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstWebsite.Data;
using MyFirstWebsite.Models;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebsite.Pages
{
    public class ContactMeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ContactMeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty, Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [BindProperty, Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [BindProperty, Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [BindProperty, Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contactMessage = new ContactMessage
            {
                Name = Name,
                Email = Email,
                Subject = Subject,
                Message = Message,
                CreatedAt = DateTime.Now
            };

            _context.ContactMessages.Add(contactMessage);
            _context.SaveChanges();

            return RedirectToPage("/MessageSent");
        }
    }
}