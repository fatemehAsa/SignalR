using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Models.Contexts
{
    public class AuthenticationContext : IdentityDbContext<AspNetUser, AspNetRole, int, AspNetUserClaim, AspNetUserRole, AspNetUserLogin,
        AspNetRoleClaim, AspNetUserToken>
    {
        public AuthenticationContext(DbContextOptions<MainContext> options) : base(options)
        {

        }
    }
}
