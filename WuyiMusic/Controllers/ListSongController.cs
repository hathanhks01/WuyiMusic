using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using WuyiDAL.Models;

namespace WuyiMusic.Controllers
{
    public class ListSongController : Controller
    {
        HttpClient _httpClient;
        public ListSongController()
        {
            _httpClient = new HttpClient();
        }
        // GET: ListSongController
        public async Task<IActionResult> ListSong( )
        {
            
                string url = "https://localhost:7178/api/Songs/get-all";
                var response = await _httpClient.GetStringAsync(url);
                var songs = JsonConvert.DeserializeObject<List<Song>>(response);
                return View(songs);           
            
        }
        public async Task<IActionResult> FindByName(string name)
        {
            string url = $"https://localhost:7178/api/Songs/FindByName?Title={name}";
            var response = await _httpClient.GetStringAsync(url);
            var songs = JsonConvert.DeserializeObject<List<Song>>(response);
            return View(songs);

        }
    }
}
