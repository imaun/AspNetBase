using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNetBase.Domain.Models;
using AspNetBase.Core.Configuration;
using AspNetBase.Core.Enum;
using Datiss.Common.Gaurd;

namespace AspNetBase.Persistence.Configuration {

    public static partial class IdentityConfiguration {

        public static void ConfigUser(
            this ModelBuilder builder, 
            UserSeedData userSeed, 
            IPasswordHasher<User> passwordHasher) {

            userSeed.CheckArgumentIsNull(nameof(userSeed));
            passwordHasher.CheckArgumentIsNull(nameof(passwordHasher));

            builder.Entity<User>(user => {
                user.ToTable(TableNames.Users).HasKey(_ => _.Id);

                user.Ignore(_ => _.DisplayName);
                user.Property(_ => _.FirstName).HasMaxLength(300).IsUnicode().IsRequired();
                user.Property(_ => _.LastName).HasMaxLength(300).IsUnicode().IsRequired();
                user.Property(_ => _.NationalCode).HasMaxLength(10).IsUnicode().IsRequired();
                user.Property(_ => _.UserName).HasMaxLength(300).IsUnicode().IsRequired();
                user.Property(_ => _.NormalizedUserName).HasMaxLength(300).IsUnicode().IsRequired();
                user.Property(_ => _.Email).HasMaxLength(1000).IsUnicode();
                user.Property(_ => _.PhoneNumber).HasMaxLength(20).IsUnicode();
                user.Property(_ => _.ConcurrencyStamp).IsConcurrencyToken();

                user.Ignore(_ => _.Status);
                user.Property("_statusId")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasColumnName("Status")
                    .HasDefaultValue(1);

                var newUser = new User
                {
                    Id = 1,
                    Status = UserStatus.Enabled,
                    Email = userSeed.Email,
                    NormalizedEmail = userSeed.Email.ToUpper(),
                    EmailConfirmed = true,
                    FirstName = userSeed.FirstName,
                    LastName = userSeed.LastName,
                    NationalCode = userSeed.NationalCode,
                    UserName = userSeed.UserName,
                    NormalizedUserName = userSeed.UserName.ToUpper(),
                    PhoneNumber = userSeed.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    TwoFactorEnabled = false, //TODO : read from config
                };
                newUser.PasswordHash = passwordHasher.HashPassword(newUser, userSeed.Password);

                user.HasData(newUser);

            });
        }

        public static void ConfigUserClaim(this ModelBuilder builder) {
            builder.Entity<UserClaim>(claim => {
                claim.ToTable(TableNames.UserClaims).HasKey(_ => _.Id);

                claim.Property(_ => _.ClaimType).HasMaxLength(100).IsUnicode().IsRequired();
                claim.Property(_ => _.ClaimValue).HasMaxLength(4000).IsUnicode();

                claim.HasOne(_ => _.User)
                    .WithMany(_ => _.Claims)
                    .HasForeignKey(_ => _.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public static void ConfigUserLoign(this ModelBuilder builder) {
            builder.Entity<UserLogin>(login => {
                login.ToTable(TableNames.UserLogins)
                        .HasKey(_=> new { _.LoginProvider, _.ProviderKey });

                login.Property(_ => _.IpAddress).HasMaxLength(100).IsUnicode().IsRequired();
                login.Property(_ => _.UserAgent).HasMaxLength(500).IsUnicode();
                login.Property(_ => _.Description).HasMaxLength(1000).IsUnicode();

                login.HasOne(_ => _.User)
                    .WithMany(_ => _.Logins)
                    .HasForeignKey(_ => _.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public static void ConfigUserRole(this ModelBuilder builder) {
            builder.Entity<UserRole>(role => {
                role.ToTable(TableNames.UserRoles)
                    .HasKey(_ => new { _.UserId, _.RoleId });

                role.HasOne(_ => _.User)
                    .WithMany(_ => _.Roles)
                    .HasForeignKey(_ => _.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                role.HasOne(_ => _.Role)
                    .WithMany(_ => _.Users)
                    .HasForeignKey(_ => _.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);

                role.HasData(new UserRole
                {
                    RoleId = 1,
                    UserId = 1
                });

            });
        }

        public static void ConfigUserToken(this ModelBuilder builder) {
            builder.Entity<UserToken>(token => {
                token.ToTable(TableNames.UserTokens);

                token.HasOne(_ => _.User)
                    .WithMany(_ => _.Tokens)
                    .HasForeignKey(_ => _.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public static void ConfigUserUsedPassword(this ModelBuilder builder) {
            builder.Entity<UserUsedPassword>(password => {
                password.ToTable(TableNames.UserUsedPassword).HasKey(_ => _.Id);

                password.Property(_ => _.HashedPassword).HasMaxLength(2000).IsRequired();
                password.HasOne(_ => _.User)
                    .WithMany(_ => _.UsedPasswords)
                    .HasForeignKey(_ => _.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
