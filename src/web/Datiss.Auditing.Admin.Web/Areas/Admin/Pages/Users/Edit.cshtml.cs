using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspNetBase.Identity.Commands;
using AspNetBase.Identity.Query;
using MediatR;
using Mapster;

namespace AspNetBase.Admin.Web
{
    public class EditUserPage : PageModel
    {

        private readonly IMediator _mediator;

        public EditUserPage(IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Properties

        [BindProperty]
        public int Id { get; set; }

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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var query = new GetUserByIdQuery(id);
            try {
                var result = await _mediator.Send(query);
                Id = result.Id;
                FirstName = result.FirstName;
                LastName = result.LastName;
                UserName = result.UserName;
                PhoneNumber = result.PhoneNumber;
                NationalCode = result.NationalCode;
            }
            catch(NullReferenceException) {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            var command = new UpdateUserCommand(
                Id,
                UserName,
                FirstName,
                LastName,
                NationalCode,
                PhoneNumber);

            try {
                var result = await _mediator.Send(command);

            }
            catch(NullReferenceException) {
                return NotFound();
            }

            return Page();
        }

    }
}
