using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNetBase.Domain.Models;
using AspNetBase.Core.Configuration;
using AspNetBase.Identity.Claims;
using Datiss.Common.Gaurd;

namespace AspNetBase.Persistence.Configuration {

    public static partial class IdentityConfiguration {

        public static void ConfigRole(
            this ModelBuilder builder,
            RoleSeedData roleSeed,
            IPasswordHasher<User> passwordHasher) {

            roleSeed.CheckArgumentIsNull(nameof(roleSeed));
            passwordHasher.CheckArgumentIsNull(nameof(passwordHasher));

            builder.Entity<Role>(role => {
                role.ToTable(TableNames.Roles).HasKey(_=> _.Id);

                role.Property(_ => _.Name).HasMaxLength(255).IsUnicode().IsRequired();
                role.Property(_ => _.NormalizedName).HasMaxLength(255).IsUnicode().IsRequired();
                role.Property(_ => _.Title).HasMaxLength(255).IsUnicode().IsRequired();
                role.Property(_ => _.Description).HasMaxLength(1000).IsUnicode();

                role.HasData(new Role
                {
                    Id = 1,
                    Name = roleSeed.Name,
                    Title = roleSeed.Title,
                    NormalizedName = roleSeed.Name.ToUpper(),
                    Description = ""
                });
            });
        }

        public static void ConfigRoleClaim(this ModelBuilder builder) {
            builder.Entity<RoleClaim>(claim => {
                claim.ToTable(TableNames.RoleClaim).HasKey(_ => _.Id);

                claim.Property(_ => _.ClaimType).HasMaxLength(100).IsUnicode().IsRequired();
                claim.Property(_ => _.ClaimValue).HasMaxLength(4000).IsUnicode();

                claim.HasOne(_ => _.Role)
                    .WithMany(_ => _.Claims)
                    .HasForeignKey(_ => _.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public static void ConfigAppClaimType(this ModelBuilder builder) {
            builder.Entity<AppClaimType>(claim => {
                claim.ToTable(TableNames.AppClaimType).HasKey(_ => _.Id);

                claim.Property(_ => _.Name).HasMaxLength(100).IsUnicode().IsRequired();
                claim.Property(_ => _.Title).HasMaxLength(255).IsUnicode();
                claim.Property(_ => _.Description).HasMaxLength(500).IsUnicode();

                claim.HasData(new AppClaimType[]
                {
                    new AppClaimType
                    {
                        Id = 1,
                        Name = AuditingClaims.Users,
                        Title = "کاربران",
                        Description = "مدیریت کاربران"
                    },
                    new AppClaimType
                    {
                        Id = 2,
                        Name = AuditingClaims.Roles,
                        Title = "نقش های کاربری",
                        Description = "مدیریت نقش ها و دسترسی های کاربران"
                    }
                });
            });
        }

    }
}
