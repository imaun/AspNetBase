using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;

namespace AspNetBase.Identity {

    public class ConfirmEmailDataProtectionTokenProviderOptions
        : DataProtectionTokenProviderOptions
    { }

    public class ConfirmEmailDataProtectorTokenProvider<TUser>
        : DataProtectorTokenProvider<TUser> where TUser : class {

        public ConfirmEmailDataProtectorTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<ConfirmEmailDataProtectionTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider, options, logger) {
            
        }

    }
}
