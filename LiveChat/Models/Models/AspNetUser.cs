using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class AspNetUser:IdentityUser<int>
    {
        public ICollection<AspNetUserRole> ApplicationUserRoles { get; set; }
    }
}
