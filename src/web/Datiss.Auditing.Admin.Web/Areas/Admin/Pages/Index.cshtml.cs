using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Datiss.Auditing.Admin.Web
{
    //[Authorize(policy: "AdminPolicy")]
    public class AdminIndexPage : PageModel
    {
        public void OnGet() {

        }
    }
}