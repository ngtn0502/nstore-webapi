namespace API.test;

public class ProductsControllerTests
{
   // private readonly ProductsController _productsController;
   // private readonly Mock<IProductRepository> _repos;
   // public ProductsControllerTests()
   // {
   //    _repos = new Mock<IProductRepository>();
   //    _productsController = new ProductsController(_repos.Object);
   // }

   // [Test]
   // public async Task GetProducts_ShouldReturnProductList_WhenProductExistAsync()
   // {
   //    // Arrange
   //    Product product = new Product
   //    {
   //       Id = 1,
   //       Name = "Algo Book"
   //    };
   //    List<Product> ltsProducts = new List<Product>();
   //    ltsProducts.Add(product);
   //    _repos.Setup(x => x.GetProductsAsync()).ReturnsAsync(ltsProducts);
   //    //Act
   //    var productList = await _productsController.GetProducts();
   //    //Assert
   //    var respondValue = (OkObjectResult)productList.Result;
   //    Assert.NotNull(respondValue.Value);
   // }

   // [Test]
   // public async Task GetProduct_ShouldReturnSingleProduct_WhenProductIdExistAsync()
   // {
   //    // Arrange
   //    var productId = 1;
   //    Product product = new Product
   //    {
   //       Id = 1,
   //       Name = "Algo Book"
   //    };
   //    _repos.Setup(x => x.GetProductByIdAsync(productId)).ReturnsAsync(product);
   //    //Act
   //    var productList = await _productsController.GetProduct(productId);
   //    //Assert
   //    var respondValue = (Product)productList.Value;
   //    Assert.AreEqual(respondValue.Id, productId);
   // }
}