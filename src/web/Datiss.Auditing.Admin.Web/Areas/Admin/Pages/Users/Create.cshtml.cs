using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspNetBase.Identity.Commands;
using MediatR;

namespace AspNetBase.Admin.Web
{
    public class CreateUserPage : PageModel
    {

        private readonly IMediator _mediator;

        public CreateUserPage(IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Properties

        [BindProperty]
        [Required]
        [MaxLength(300)]
        public string FirstName { get; set; }

        [BindProperty]
        [Required]
        [MaxLength(300)]
        public string LastName { get; set; }

        [BindProperty]
        [Required]
        [MaxLength(300)]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [MaxLength(10)]
        public string NationalCode { get; set; }

        [BindProperty]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        #endregion

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return RedirectToPage();
            }

            var command = new CreateUserCommand(
                UserName,
                FirstName,
                LastName,
                NationalCode);

            var result = await _mediator.Send(command);

            return Page();
        }
    }
}
