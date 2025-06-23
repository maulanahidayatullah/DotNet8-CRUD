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
    }
}
