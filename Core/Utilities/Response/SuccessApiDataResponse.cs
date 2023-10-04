namespace Core.Utilities.Response
{
    public class SuccessApiDataResponse<T> : ApiDataResponse<T>
    {
        public SuccessApiDataResponse(T data):base(success:true)
        {
                Data = data;
        }

        public SuccessApiDataResponse(T data, string message) : base(success: true, message)
        {
            Data=data;
        }

    }
}
