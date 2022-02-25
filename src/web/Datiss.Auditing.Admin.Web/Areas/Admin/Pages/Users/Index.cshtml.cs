using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspNetBase.Core.Models;
using AspNetBase.Identity.Query;
using AspNetBase.Identity.Models;
using AspNetBase.Admin.Web.Models;
using Datiss.Common.Gaurd;
using AspNetBase.Core;
using AspNetBase.Core.Enum;
using MediatR;

namespace AspNetBase.Admin.Web
{
    public class UserIndexPage : PageModel
    {

        private readonly IMediator _mediator;

        public UserIndexPage(IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Properties
        //[BindProperty]
        //public string UserName { get; set; }

        //[BindProperty]
        //public string FirstName { get; set; }

        //[BindProperty]
        //public string LastName { get; set; }

        //[BindProperty]
        //public string NationalCode { get; set; }

        //[BindProperty]
        //public string PhoneNumber { get; set; }

        //[BindProperty]
        //public int PageSize { get; set; } = 10;

        //[BindProperty]
        //public int PageNumber { get; set; } = 1;

        //public UserListResult List { get; set; }
        
        [BindProperty]
        public UserListFilter Filter { get; set; }

        #endregion

        public async Task<IActionResult> OnGetAsync(int page = 1)
        {
            //var query = new GetUserListQuery(page, PageSize, "id", true);
            //List = await _mediator.Send(query);
            Filter = new UserListFilter();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {

            return Page();
        }


    }
}
