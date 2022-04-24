using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspNetBase.Resources;
using AspNetBase.Web.Core;
using AspNetBase.Admin.Web.Models;
using AspNetBase.Identity.Query;
using Mapster;
using MediatR;

namespace AspNetBase.Admin.Web {

    public class CreateRolePage : BasePageModel {

        private readonly IMediator _mediator;

        public CreateRolePage(IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Properties

        [Required(ErrorMessageResourceType = typeof(ViewModelStrings), ErrorMessageResourceName = "Required")]
        [MaxLength(255, ErrorMessageResourceType = typeof(ViewModelStrings), ErrorMessageResourceName = "MaxLen")]
        [BindProperty]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewModelStrings), ErrorMessageResourceName = "Required")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ViewModelStrings), ErrorMessageResourceName = "MaxLen")]
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Description { get; set; }
        
        public IEnumerable<AppcClaimTypeViewModel> ClaimTypeSource { get; set; }
        public Dictionary<string, string> SelectedClaims { get; set; }

        #endregion

        public async Task<IActionResult> OnGetAsync()
        {
            ClaimTypeSource = (await _mediator.Send(new GetAppClaimTypesQuery()))
                                                .Adapt<List<AppClaimTypeViewModel>>();
            foreach (var claim in ClaimTypeSource) {
                SelectedClaims.Add(claim.Name, "");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {

            return Page();
        }

    }
}
