namespace API.Errors
{
   public class ApiResponse
   {
      public int StatusCode { get; set; }
      public string Message { get; set; }

      public ApiResponse(int statusCode, string? message = null)
      {
         StatusCode = statusCode;
         Message = message ?? GetDefaultMessage(statusCode);
      }

      private string GetDefaultMessage(int statusCode)
      {
         return statusCode switch
         {
            400 => "Bad request error happened!",
            404 => "Not found error happened!",
            500 => "Server error happened!",
            _ => "Error happened!",
         };
      }
   }
}