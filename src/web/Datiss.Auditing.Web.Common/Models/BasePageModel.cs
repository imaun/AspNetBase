using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetBase.Web.Core {

    public abstract class BasePageModel : PageModel {

        public bool HasError { get; protected set; }
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        public void Succeed(string message = "") {
            Success = true;
            HasError = !Success;
            Message = message;
        }

        public void AddError(string message) {
            HasError = true;
            Success = !HasError;
            Message = message;
        }

    }
}
