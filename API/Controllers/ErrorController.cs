using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class ErrorController : BaseApiController
   {
      [HttpGet("{statusCode}")]
      [ApiExplorerSettings(IgnoreApi = true)]
      public ObjectResult GetError(int statusCode)
      {
         return new ObjectResult(new ApiResponse(statusCode));
      }

   }
}