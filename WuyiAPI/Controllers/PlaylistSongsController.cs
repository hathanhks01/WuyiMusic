using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WuyiDAL.Models;
using WuyiServices.IServices;

namespace WuyiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistSongsController : ControllerBase
    {
        private readonly IServices<PlaylistSong> _services;

        public PlaylistSongsController(IServices<PlaylistSong> services)
        {
            _services = services;
        }

        // GET: api/PlaylistSongs
        [HttpGet("get-all")]
        public async Task<ActionResult<ICollection<User>>> GetPlaylistSongs()
        {
            var playlistSongs = await _services.GetAllAsync();
            return Ok(playlistSongs);
        }

        // GET: api/PlaylistSongs/{id}
        [HttpGet("get-by-id")]
        public async Task<ActionResult<PlaylistSong>> GetPlaylistSong(Guid id)
        {
            try
            {
                var playlistSong = await _services.GetByIdAsync(id);
                if (playlistSong == null)
                {
                    return NotFound();
                }
                return Ok(playlistSong);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
        }



        // POST: api/PlaylistSongs
        [HttpPost("create")]
        public async Task<ActionResult> PostUser(PlaylistSong playlistSong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _services.CreateAsync(playlistSong);
                if (!result)
                {
                    return BadRequest("Could not create the PlaylistSong.");
                }

                return CreatedAtAction(nameof(GetPlaylistSong), new { id = playlistSong.PlaylistId }, playlistSong);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error.");
            }
        }
        // PUT: api/PlaylistSongs/{id}
        [HttpPut("update")]
        public async Task<ActionResult> PutPlaylistSong(PlaylistSong playlistSong)
        {
            try
            {

                var playlistSongEdit = await _services.GetByIdAsync(playlistSong.PlaylistId);
                if (playlistSongEdit == null)
                {
                    return NotFound();
                }
                playlistSongEdit.PlaylistId = playlistSong.PlaylistId;
                playlistSongEdit.SongId = playlistSong.SongId;
                await _services.UpdateAsync(playlistSongEdit);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Log the exception (ex)
                return BadRequest();
            }
        }

        // DELETE: api/PlaylistSongs/{id}
        [HttpDelete("delete")]
        public async Task<ActionResult> DeletePlaylistSong(Guid id)
        {
            try
            {
                var PlaylistSong = await _services.GetByIdAsync(id);
                if (PlaylistSong == null)
                {
                    return NotFound();
                }

                var result = await _services.DeleteAsync(PlaylistSong);
                if (!result)
                {
                    return BadRequest("Could not delete the PlaylistSong.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return BadRequest(ex.Message);
            }
        }
    }
}
