namespace API.Helpers
{
   public class Pagination<T>
   {
      public int PageNumber { get; set; }
      public int PageSize { get; set; }
      public int Count { get; set; }
      public IEnumerable<T>? Data { get; set; }
   }
}