using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuyiDAL.Models.systems;

namespace WuyiServices.IServices
{
    public interface IAuthenticateService
    {
        Task<string> RegisterAsync(UserRegistration userRegistration);
        Task<string> LoginAsync(UserLogin userLogin);
    }
}
