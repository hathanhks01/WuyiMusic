using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using WuyiDAL.Models;
using WuyiMusic.Models;

namespace WuyiMusic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private AppDbContext _context;
        public HomeController(ILogger<HomeController> logger,HttpClient hp)
        {
            _httpClient = hp;
            _logger = logger;
            _context = new AppDbContext();
        }

        public async Task<IActionResult> Index()
        {
            string url = "https://localhost:7178/api/Songs/get-all";
            var response = await _httpClient.GetStringAsync(url);
            var songs = JsonConvert.DeserializeObject<List<Song>>(response);
            return View(songs);
        }

   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
