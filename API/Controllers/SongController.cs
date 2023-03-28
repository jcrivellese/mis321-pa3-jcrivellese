using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using API;
using API.database;
using API.interfaces;


namespace mis321_pa3_jcrivellese.Controllers
{
    [Route("api/song")]
    [ApiController]
    public class SongController : ControllerBase
    {
        // GET: api/Song
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Song> Get()
        {
            IGetAllSongs readObject = new ReadSongData();
            return readObject.GetAllSongs();
        }

        // GET: api/Song/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "Get")]
        public Song Get(int id)
        {
            IGetSong readObject = new ReadSongData();
            return readObject.GetSong(id);
        }

        // POST: api/Song
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Song song)
        {
            ISaveSong save = new SaveSong();
            save.CreateSong(song);
        }

        // PUT: api/Song/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Song song)
        {
            ISaveSong save = new SaveSong();
            save.Save(id, song);
        }

        // DELETE: api/Song/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            IDeleteSong deleteSong = new RemoveSong();
            deleteSong.Delete(id);
        }
    }
}
