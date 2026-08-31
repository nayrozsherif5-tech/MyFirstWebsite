using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebsite.Pages
{
    public class ContactMeModel : PageModel
    {
        public int Id { get; set; }
        [BindProperty, Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [BindProperty, Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [BindProperty, Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [BindProperty, Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // redisplay with validation errors
            }

            // TODO: later add email sending or database saving here

            return RedirectToPage("/MessageSent");
        }
    }
}
