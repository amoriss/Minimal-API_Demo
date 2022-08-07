namespace MinimalAPI_Demo;

public interface IProductRepo
{
    IEnumerable<Product> GetProducts();
    Product GetProduct(int id);
    void InsertProduct(Product prod);
    void UpdateProduct(Product prod);
    void DeleteProduct(int id);
}