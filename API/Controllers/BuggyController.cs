using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class BuggyController : BaseApiController
   {
      private readonly StoreContext _context;
      public BuggyController(StoreContext context)
      {
         _context = context;
      }

      [HttpGet("notfound")]
      public IActionResult NotFoundError()
      {
         var product = _context.Products.Find(34);
         if (product == null)
         {
            return NotFound(new ApiResponse(404));
         }
         return Ok();
      }


      [HttpGet("badrequest")]
      public IActionResult BadRequestError()
      {
         var product = _context.Products.Find(34);
         if (product == null)
         {
            return BadRequest(new ApiResponse(400));
         }
         return Ok();
      }

      [HttpGet("internalserver")]
      public IActionResult InternalServerError()
      {
         var product = _context.Products.Find(34);
         product.ToString();
         return Ok();
      }


      [HttpGet("validation/{id}")]
      public IActionResult ValidationError(int id)
      {
         var product = _context.Products.Find(34);
         return Ok();
      }
   }
}