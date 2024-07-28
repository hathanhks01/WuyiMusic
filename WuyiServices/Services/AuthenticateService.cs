using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuyiDAL.IReponsitory;
using WuyiDAL.Models.systems;
using WuyiDAL.Repository;
using WuyiServices.IServices;

namespace WuyiServices.Services
{
    public class AuthenticateService: IAuthenticateService
    {
        private readonly IAuthenticateRepo _repository;


        public async Task<string> LoginAsync(UserLogin userLogin)
        {
            return await _repository.Login(userLogin);
        }



        public async Task<string> RegisterAsync(UserRegistration userRegistration)
        {
            return await _repository.Register(userRegistration);
        }
    }
}
