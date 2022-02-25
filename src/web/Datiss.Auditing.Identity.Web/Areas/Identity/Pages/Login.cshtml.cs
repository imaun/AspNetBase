using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Datiss.Auditing.Identity.Commands;
using MediatR;

namespace Datiss.Auditing.Identity.Web
{
    public class LoginPage : PageModel
    {

        private readonly IMediator _mediator;

        public LoginPage(IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        #region Properties

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        #endregion

        public async Task OnGetAsync() {

        }

        public async Task<IActionResult> OnPostAsync() {
            if(!ModelState.IsValid) {
                return RedirectToPage();
            }

            var command = new UserLoginCommand(UserName, Password, RememberMe);
            var result = await _mediator.Send(command);

            if(result.Success) {
                return RedirectToPage("/Index", new { Area = "Admin" });
            }

            ViewData["error"] = true;
            ViewData["msg"] = result.Message;
            
            return Page();
        }

    }
}
