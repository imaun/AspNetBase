using Microsoft.AspNetCore.Mvc;
using AspNetBase.Admin.Web.Models;
using AspNetBase.Identity.Models;
using AspNetBase.Identity.Query;
using AspNetBase.Web.Common;
using MediatR;

namespace AspNetBase.Admin.API {

    [Area("Admin")]
    [Produces("application/json")]
    [Route("api/[area]/users")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Index() {
            var model = Request.Form.ToDataTable<UserListFilter>();
            var filter = UserListFilter.FromForm(Request.Form);
            model.SetFilter(filter);

            var query = new GetUserListQuery(
                model.PageNumber,
                model.PageSize,
                filter.OrderKey(model.OrderBy),
                model.OrderDesc,
                model.SearchValue) {

                FullName = model.Filter.FullName,
                UserName = model.Filter.UserName,
                PhoneNumber = model.Filter.PhoneNumber,
                NationalCode = model.Filter.NationalCode,
                Status = model.Filter.Status
            };

            var resutl = await _mediator.Send(query);

            return new JsonResult(new {
                draw = model.Draw,
                recordsTotal = resutl.TotalCount,
                recordsFiltered = resutl.ItemsCount,
                data = resutl.Items
            });
        }

    }
}
