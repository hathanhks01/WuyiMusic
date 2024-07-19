using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WuyiDAL.Models;

namespace WuyiMusic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenresController : Controller
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        public GenresController(AppDbContext context,HttpClient hp)
        {
            _httpClient = hp;
        }

        // GET: Admin/Genres
        public async Task<IActionResult> Index()
        {
            List<Genre> genre = new List<Genre>();
            string url = "https://localhost:7178/api/Genres/get-all";
            var reponse = await _httpClient.GetStringAsync(url);
            genre = JsonConvert.DeserializeObject<List<Genre>>(reponse);
            return View(genre);
        }

        // GET: Admin/Genres/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            string url = $"https://localhost:7178/api/Genres/get-by-id:id=?{id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var genre = JsonConvert.DeserializeObject<Genre>(reponse);
            return View(genre);
        }

        // GET: Admin/Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreId,Name")] Genre genre)
        {
            string url = $"https://localhost:7178/api/Genres/create";
            genre.GenreId = new Guid();
            var reponse = await _httpClient.PostAsJsonAsync(url, genre);
            return RedirectToAction("Index" );
        }

        // GET: Admin/Genres/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            string url = "https://localhost:7178/api/Genres/get-by-id:id=?{id}";
            var reponse = await _httpClient.GetStringAsync(url);
            var genre = JsonConvert.DeserializeObject<Genre>(reponse);
            return View(genre);
        }

        // POST: Admin/Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GenreId,Name")] Genre genre)
        {
            string url = $"https://localhost:7178/api/Genres/update";
            genre.GenreId = new Guid();
            var reponse = await _httpClient.PutAsJsonAsync(url, genre);
            return RedirectToAction("Index");
        }

        // GET: Admin/Genres/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            string url = $"https://localhost:7178/api/Genres/delete:id=?{id}";
            var reponse = await _httpClient.DeleteAsync(url);
            return RedirectToAction("Index");
        }
    }

        

       
    
}
