using Dotnet_AnimeCRUD.Data;
using Dotnet_AnimeCRUD.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_AnimeCRUD.Controllers
{
    // Bisa diganti buat nama api nya
    [Route("api/anime")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        // menginject datacontext agar bisa dipakai di controller
        // dengan menggunakan constructor
        private readonly DataContext _dataContext;
        public AnimeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // !!! yang inject context langsung ini hanya contoh !!!
        // !!! selanjutnya mungkin akan menggunakan service ama repository !!!
        // !!! jdi context ini harusnya ditaruh di service atau repository !!!
        // !!! dan controller hanya mengambil inject dari service atau repository !!!

        //Bisa diganti misal nnti buat detail
        [HttpGet]
        public async Task<ActionResult<List<Anime>>> GetAllAnime()
            // pakai ActionResult biar fe bisa tahu example data apa saja yg nnti akan muncul
            // atau pakai IActionResult tpi fe ngga bisa tahu data apa yg muncul
        {
            // awal tidak pakai db
            //var anime = new List<Anime>
            //{
            //    new Anime
            //    {
            //        Id = 1,
            //        Tittle = "Hatsune Miku",
            //        Description = "Hatsune Miku is vocaloid singer"
            //    }
            //};

            // pakai db
            var animes = await _dataContext.Animes.ToListAsync();

            return Ok(animes);
        }

        // Biar dapet id dari params
        [HttpGet("detail/{id}")]
        // ActionResult tidak pakai list buat di swaggernya dia dapet response object
        public async Task<ActionResult<Anime>> GetDetailAnime(int id)
        {
            var anime = await _dataContext.Animes.FindAsync(id);
            if (anime is null)
            {
                return NotFound("Anime Not Found!");
            }

            return Ok(anime);
        }

        [HttpPost("create")]
        // Request yg Anime di param harusnya pakai DTO (Data Transfer Object)
        // Jdi Request sama Ressponse modelnya dibedain
        public async Task<ActionResult> CreateAnime([FromBody] Anime anime)
        {
            // pertama dia akan menyimpan dlu di entities
            // abis itu dia commit ke db nya agar bisa di save
            _dataContext.Animes.Add(anime);
            await _dataContext.SaveChangesAsync();

            return Ok("Created Successfully!");
        }

        [HttpPut("update/{id}")]
        // Request yg Anime di param harusnya pakai DTO (Data Transfer Object)
        // Jdi Request sama Ressponse modelnya dibedain
        public async Task<ActionResult> UpdateAnime([FromBody] Anime animeReq, int id)
        {
            var animeData = await _dataContext.Animes.FindAsync(id);
            if (animeData is null)
            {
                return NotFound("Anime Not Found!");
            }

            animeData.Tittle = animeReq.Tittle;
            animeData.Description = animeReq.Description;

            await _dataContext.SaveChangesAsync();

            return Ok("Updated Successfully!");
        }

        [HttpDelete("delete/{id}")]
        // Request yg Anime di param harusnya pakai DTO (Data Transfer Object)
        // Jdi Request sama Ressponse modelnya dibedain
        public async Task<ActionResult> DeleteAnime( int id)
        {
            var animeData = await _dataContext.Animes.FindAsync(id);
            if (animeData is null)
            {
                return NotFound("Anime Not Found!");
            }

            _dataContext.Animes.Remove(animeData);
            await _dataContext.SaveChangesAsync();

            return Ok("Deleted Successfully!");
        }
    }
}
