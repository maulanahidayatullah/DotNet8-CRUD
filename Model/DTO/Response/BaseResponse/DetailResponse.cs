namespace Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse
{
    public class DetailResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
