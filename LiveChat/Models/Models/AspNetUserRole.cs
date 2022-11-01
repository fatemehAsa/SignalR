using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class AspNetUserRole : IdentityUserRole<int>
    {
        public AspNetUser ApplicationUser { get; set; }

        public AspNetRole ApplicationRole { get; set; }
    }
}
