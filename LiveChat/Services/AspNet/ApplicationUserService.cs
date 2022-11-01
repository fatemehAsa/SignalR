using Models.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Services.AspNet
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly MainContext _context;
        public ApplicationUserService(MainContext context)
        {
            _context = context;
        }

        public List<string> GetRole(string name)
        {
            int userId = _context.AspNetUsers.FirstOrDefault(u => u.UserName == name).Id;
            List<int> roleIds = _context.AspNetUserRoles.Where(e => e.UserId == userId)
                .Select(e => e.RoleId).ToList();
            var roleTitles = _context.AspNetRoles.Where(r => roleIds.Contains(r.Id)).Select(r => r.Name).ToList();
            return roleTitles;
        }
    }
}
