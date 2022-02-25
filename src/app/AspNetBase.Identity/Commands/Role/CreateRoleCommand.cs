using MediatR;
using Datiss.Common.Utils;
using Datiss.Common.Gaurd;
using AspNetBase.Identity.Models;
using AspNetBase.Contracts.Identity;

namespace AspNetBase.Identity.Commands {

    public class CreateRoleCommand : IRequest<Result<RoleResult>>
    {


        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

}
