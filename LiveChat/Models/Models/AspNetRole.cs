using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class AspNetRole : IdentityRole<int>
    {
        public ICollection<AspNetUserRole> ApplicationUserRoles { get; set; }
    }
}
