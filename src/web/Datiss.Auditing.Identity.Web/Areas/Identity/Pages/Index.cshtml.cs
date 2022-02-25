using Microsoft.AspNetCore.Mvc.RazorPages;
using Datiss.Auditing.Identity.Commands;
using MediatR;

namespace Datiss.Auditing.Identity.Web
{
    public class IndexModel : PageModel
    {

        private readonly IMediator _mediator;

        public IndexModel(
            IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        public async Task OnGet()
        {
            var command = new CreateUserCommand("Iman", "Iman", "Nemati", "05348543111");
            var response = await _mediator.Send(command);

            if(response.HasError) {

            }

            if(response.Success) {

            }

            var ex = response.Exception;
        }

        public void OnPost() {

        }
    }
}
