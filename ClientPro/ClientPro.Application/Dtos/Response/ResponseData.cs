namespace ClientPro.Application.Dtos.Response
{
    public class ResponseData<T>
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public List<T> Data { get; set; }
    }
}
