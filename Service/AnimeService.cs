﻿using System.Xml.Linq;
using Dotnet_AnimeCRUD.Model;
using Dotnet_AnimeCRUD.Model.DTO.Filter;
using Dotnet_AnimeCRUD.Model.DTO.Request;
using Dotnet_AnimeCRUD.Model.DTO.Response;
using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;
using Dotnet_AnimeCRUD.Model.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Dotnet_AnimeCRUD.Service
{
    public class AnimeService
    {
        private readonly AnimeDBContext _dbContext; // untuk memanggil db nya
        private readonly ILogger<AnimeService> _logger; // untuk logging dari .net

        // Diharuskan pakai constructor
        // Agar di inject atau class lain bisa di pakai di class ini
        public AnimeService(ILogger<AnimeService> _logger, AnimeDBContext _dbContext)
        {
            this._logger = _logger;
            this._dbContext = _dbContext;
        }

        // Pakai Task agar dia bisa async
        // pakai PaginatedResponse agar pada swagger struktur model responsenya akan sama seperti DTO -> Response -> BaseResponse -> PaginatedResponse
        // AnimeFilter adalah yang dikirim dari Controllers -> AnimeController
        public async Task<PaginatedResponse<AnimeResponse>> GetListAnime(AnimeFilter filter)
        {
            // !!! Beda Mapping dengan Builder
            // !!! Builder = membuat objek.
            // !!! Mapping = genti objek ke bentuk lain (Response atau DTO atau Entity).

            // Inisialisasi filter yang dikirim dari Controllers -> AnimeController
            int page = filter.Page;
            int pagesize = filter.PageSize;
            int offset = (page - 1) * pagesize;

            // Buat _dbContext agar bisa dibuat menjadi query 
            var query = _dbContext.Animes.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Search))
            {
                // EF.Functions.ILike(...)
                // Digunakan untuk melakukan case-insensitive LIKE
                // kalau di sqlnya "WHERE tittle ILIKE '%search%'"
                // Sama kalau di filternya ada search dia akan menambahkan query.where
                query = query.Where(a => EF.Functions.ILike(a.Tittle, $"%{filter.Search}%"));
            }

            // Buat Pagination kemudian dimasukan ke ListAsync
            var result = await query
                .Skip(offset)
                .Take(pagesize)
                .ToListAsync();

            var totalData = await query.CountAsync();
            var maxPage = (int)Math.Ceiling((double)totalData / pagesize);

            // Mapping data yang didapat dari result
            // Kareana ini datanya banyak (array) maka kita masukan ke List (.ToList()) 
            // kemudian memasukannya ke AnimeResponse dan inisialisasi ke variabel data
            var dataMapping = result.Select(data => new AnimeResponse
            {
                Id = data.Id,
                Tittle = data.Tittle,
                Description = data.Description
            }).ToList();

            // Memasukan ke DTO -> Response -> BaseResponse -> PaginatedResponse
            // Dan Inisialisasikan ke variable response
            PaginatedResponse<AnimeResponse> response = new PaginatedResponse<AnimeResponse>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data Retrieved Successfully!",
                Page = page,
                PageSize = pagesize,
                TotalData = totalData,
                MaxPage = maxPage, 
                Data = dataMapping
            };

            return response;
        }
        
        // Akan mereturn DetailResponse dengan turunan dari BaseResponse
        // Dan untuk object data dari DetailResponse untuk struktur modelnya akan menyamakan dari AnimeResponse
        public async Task<DetailResponse<AnimeResponse>> GetDetailAnime(int id)
        {
            // !!! Beda Mapping dengan Builder
            // !!! Builder = membuat objek.
            // !!! Mapping = genti objek ke bentuk lain (Response atau DTO atau Entity).

            var result = await _dbContext.Animes.FindAsync(id);

            if (result is null)
            {
                // Kalau tidak ditemukan response Data nya akan mengembalikan AnimeResponse dengan data default yang sudah di atur di
                // DTO -> Response -> AnimeResponse
                return new DetailResponse<AnimeResponse>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Data Not Found!",
                    Data = new AnimeResponse()
                };
            }


            // Kalau datanya ada maka akan di mapping atau masukan dlu ke AnimeResponse yang datanya didapat dari query 
            var dataMapping = new AnimeResponse
            {
                Id = result.Id,
                Tittle = result.Tittle,
                Description = result.Description
            };

            // Kemudian yang sudah di mapping tadi dimasukan ke object data DetailResponse
            var response = new DetailResponse<AnimeResponse>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data Retrieved Successfully!",
                Data = dataMapping
            };

            return response;
        }

        public async Task<BaseResponse> CreateAnime(AnimeRequest request)
        {
            // !!! Beda Mapping dengan Builder
            // !!! Builder = membuat objek.
            // !!! Mapping = genti objek ke bentuk lain (Response atau DTO atau Entity).

            // Masukan dulu request ke mapping entity
            // Agar bisa di add
            // Karena fungsi add itu hanya menerima entity kalau dari DTO dia tidak mau :v
            var dataMapping = new Anime
            {
                Tittle = request.Tittle,
                Description = request.Description
            };

            // Tambahkan dataMapping tadi ke fungsi Add
            _dbContext.Animes.Add(dataMapping);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = true,
                StatusCode = 200,
                Message = "Kntol Created!",
            };
        }
        public async Task<BaseResponse> UpdateAnime(int id, AnimeRequest request)
        {
            // Akan mencari data anime berdasarkan id yg dari parameter
            var result = await _dbContext.Animes.FindAsync(id);
            if (result is null)
            {
                // Kalau datanya kosong akan return BaseResponse "Not Found"

                return BaseResponse.NotFound();
                //return new BaseResponse()
                //{
                //    Status = false,
                //    StatusCode = 404,
                //    Message = "Data Not Found!",
                //};
            }

            // Kalau update TIDAK PERLU DI MAPPING :v
            // Mengisi result dengan data dari request
            result.Tittle = request.Tittle;
            result.Description = request.Description;

            // Mengupdate datanya
            await _dbContext.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = true,
                StatusCode = 200,
                Message = "Successfully Updated!",
            };
        }

        public async Task<BaseResponse> DeleteAnime(int id)
        {

            // Akan mencari data anime berdasarkan id yg dari parameter
            var result = await _dbContext.Animes.FindAsync(id);
            if (result is null)
            {
                // Kalau datanya kosong akan return BaseResponse "Not Found"
                return new BaseResponse()
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Data Not Found!",
                };
            }

            // data yang didapat dari query (result) akan langsung diapakai pada _dbContext Remove
            _dbContext.Animes.Remove(result);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = true,
                StatusCode = 200,
                Message = "Successfully Deleted!",
            };
        }

    }
}
