namespace Domain.Entities.Pagination
{
    public class ApiResponse<T>
    {
        public List<T> Data { get; set; }
        public T SingleItem { get; set; }
        public int TotalSize { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public int TotalPages { get; set; }
        public string NextPageLink { get; set; }
        public string PreviousPageLink { get; set; }

        public ApiResponse() { }

        public static ApiResponse<T> CreateForList(List<T> data, int totalSize, int totalPages, string nextPageLink = null, string previousPageLink = null, string message = "Success", bool status = true)
        {
            return new ApiResponse<T>
            {
                Data = data,
                TotalSize = totalSize,
                TotalPages = totalPages,
                NextPageLink = nextPageLink,
                PreviousPageLink = previousPageLink,
                Message = message,
                Status = status
            };
        }

        public static ApiResponse<T> CreateForSingle(T singleItem, string message = "Success", bool status = true)
        {
            return new ApiResponse<T>
            {
                SingleItem = singleItem,
                Message = message,
                Status = status
            };
        }
    }
}
