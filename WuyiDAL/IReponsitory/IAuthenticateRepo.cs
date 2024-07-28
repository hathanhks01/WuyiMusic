using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuyiDAL.Models.systems;

namespace WuyiDAL.IReponsitory
{
    public interface IAuthenticateRepo
    {
       public Task<string> Register(UserRegistration userRegistration);
        public Task<string> Login(UserLogin userLogin);

    }

}
