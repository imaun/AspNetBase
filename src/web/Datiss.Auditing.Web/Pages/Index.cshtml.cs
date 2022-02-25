using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;

namespace Datiss.Auditing.Web.Pages
{

    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(
            ILogger<IndexModel> logger,
            IMediator mediator) {
            _logger = logger;
            _mediator = mediator;
        }

        public void OnGet() {
        }

    }
}