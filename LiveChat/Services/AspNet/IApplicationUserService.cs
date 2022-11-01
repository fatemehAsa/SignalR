using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.AspNet
{
    public interface IApplicationUserService
    {
        List<string> GetRole(string name);
    }
}
