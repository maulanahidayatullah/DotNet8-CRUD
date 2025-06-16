using System.Text.Json.Serialization;

namespace Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse
{
    public class BaseResponse
    {
        // JsonPropertyOrder
        // Agar urutan json pada swagger dia yang paling pertama :v

        [JsonPropertyOrder(-5)]
        public Boolean? Status { set; get; }
        [JsonPropertyOrder(-4)]
        public int? StatusCode { set; get; }
        [JsonPropertyOrder(-3)]
        public string? Message { set; get; }

        public static BaseResponse Ok(string message)
        => new()
        {
            Status = true,
            StatusCode = 200,
            Message = "OK"
        };
        public static BaseResponse NotFound()
        => new()
        {
            Status = false,
            StatusCode = 404,
            Message = "Data Not Found!"
        };
    }
}
