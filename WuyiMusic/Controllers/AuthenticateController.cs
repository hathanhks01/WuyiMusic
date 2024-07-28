using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WuyiDAL.Models.systems;
using WuyiMusic.Models;

namespace WuyiMusic.Controllers
{
    public class AuthenticateController : Controller
    {
        HttpClient _httpClient;
        public AuthenticateController(HttpClient hp)
        {
            _httpClient =  hp;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7178/api/Authenticate/register", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                var token = tokenResponse?.Token;

                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Session.SetString("JWToken", token);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Registration Attempt");
            return RedirectToAction("Index","Songs");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7178/api/Authenticate/login", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenResponse>(result).Token;
                // Lưu token vào session hoặc cookie
                HttpContext.Session.SetString("JWToken", token);

                return RedirectToAction("ListSong", "ListSong");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(model);
        }
    }


}

