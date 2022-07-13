using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class ErrorController : BaseApiController
   {
      [HttpGet("{statusCode}")]
      public ObjectResult GetError(int statusCode)
      {
         return new ObjectResult(new ApiResponse(statusCode));
      }

   }
}