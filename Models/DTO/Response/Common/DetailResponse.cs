using System.Text.Json.Serialization;

namespace Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse
{
    public class DetailResponse<T> : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }
    }
}
