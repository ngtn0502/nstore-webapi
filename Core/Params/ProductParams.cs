namespace Core.Params
{
   public class ProductParams
   {
      const int MAX_PAGE_SIZE = 30;
      private int _pageSize = 5;
      public int PageSize
      {
         get => _pageSize;
         set
         {
            _pageSize = value > MAX_PAGE_SIZE
                ? MAX_PAGE_SIZE
                : value;
         }
      }

      public int PageNumber { get; set; } = 1;
      public string? Sort { get; set; }
      public int? BrandId { get; set; }
      public int? TypeId { get; set; }
      private string? _search;

      public string? Search
      {
         get => _search;
         set => _search = value?.ToLower();
      }
   }
}