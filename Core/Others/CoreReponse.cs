namespace Core.Others
{
    public class CoreResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public CoreResponse(string errorMessage)
        {
            this.Success = false;
            this.Message = errorMessage;
        }

        public CoreResponse(T data)
        {
            this.Success = true;
            this.Data = data;
        }

        public CoreResponse(T data, string successMessage)
        {
            this.Success = true;
            this.Data = data;
            this.Message = successMessage;
        }
    }
}
