using Dotnet_AnimeCRUD.Model;
using Dotnet_AnimeCRUD.Model.DTO.Filter;
using Dotnet_AnimeCRUD.Model.DTO.Filter.BaseFilter;
using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;
using Dotnet_AnimeCRUD.Model.Entity;
using Dotnet_AnimeCRUD.Models.DTO.Request;
using Dotnet_AnimeCRUD.Models.DTO.Request.Anime;
using Dotnet_AnimeCRUD.Models.DTO.Response.Anime;
using Dotnet_AnimeCRUD.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_AnimeCRUD.Controllers
{
    // Bisa diganti buat nama api nya
    [Authorize(Roles = "Admin")]
    [Route("api/anime")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        // menginject AnimeService agar bisa dipanggil atau dipakai di controller
        // Diharuskan pakai constructor
        // Agar di inject atau class lain bisa di pakai di class ini
        
        private readonly AnimeService animeService;
        public AnimeController(AnimeService animeService)
        {
            this.animeService = animeService;
        }

        // !! Kenapa pakai AnimeResponse? !!
        // !! Karena barangkali nnti responsenya ada array !!
        // !! Soalnya kalau dari entity dia HANYA UNTUK COLUMN YANG ADA PADA TABEL !!

        // Akan mengembalikan PaginatedResponse yang ada pada DTO -> Response -> BaseResponse -> PaginatedResponse
        // Dengan nnti ada inputan pada query dengan inputannya ada pada DTO -> Filter -> AnimeFilter yg berisi : 
        // Search, dan turunan atau isi dari DTO -> Filter -> BaseFilter -> PaginatedFilter yg berisi jga Page & PageSize
        // Makanya di swagger akan ada inputan query Page & PageSize
        // Kenapa pakai AnimeResponse
        // Agar object yang ada pada PaginatedResponse struktur modelnya akan sama seperti AnimeResponse
        //[Authorize(Roles = "User")] // Authorize hanya akun admin yang bisa
        [HttpGet]
        public async Task<PaginatedResponse<ListAnimeResponse>> GetListAnime([FromQuery] AnimeFilter filter)
        {
            // Dia akan Menginject Service -> AnimeService 
            // Dengan mengirim filter yang ada pada parameter
            return await animeService.GetListAnime(filter);
        }

        // Biar dapet id dari params
        // Kenapa pakai AnimeResponse
        // Agar object yang ada pada PaginatedResponse struktur modelnya akan sama seperti AnimeResponse
        //[Authorize(Roles = "Admin")] // Authorize hanya akun admin yang bisa
        [HttpGet("detail/{id}")]
        public async Task<DetailResponse<DetailAnimeResponse>> GetDetailAnime(int id)
        {
            // Akan mengirim id ke AnimeService -> DetailAnime
            return await animeService.GetDetailAnime(id);
        }

        [HttpPost("create")]
        public async Task<BaseResponse> CreateAnime([FromBody] CreateAnimeRequest request)
        {
            // Akan mengirim request ke AnimeService -> CreateAnime
            return await animeService.CreateAnime(request);
        }

        [HttpPut("update/{id}")]
        public async Task<BaseResponse> UpdateAnime(int id, [FromBody] UpdateAnimeRequest request)
        {
            // Akan mengirim id dari param dan request dari body ke AnimeService -> UpdateAnime
            return await animeService.UpdateAnime(id, request);
        }

        [HttpDelete("delete/{id}")]
        public async Task<BaseResponse> DeleteAnime(int id)
        {
            return await animeService.DeleteAnime(id);
        }
    }
}
