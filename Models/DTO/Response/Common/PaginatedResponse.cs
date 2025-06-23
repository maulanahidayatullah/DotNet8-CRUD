namespace Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse
{
    public class PaginatedResponse<T> : BaseResponse
    {
        public PaginatedResponse()
        {
            Data = new List<T>();
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalData { get; set; }
        public int MaxPage { get; set; }
        public ICollection<T> Data { set; get; }

        // Function untuk menambahkan Data karena bisa saja Datanya kosong
        public void AddData(T t)
        {
            if (Data == null)
            {
                Data = new List<T>();
            }

            Data.Add(t);
        }
    }
}
