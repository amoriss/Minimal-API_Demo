public interface IProductRepo
{
    void DeleteProduct(Product prod);
    Product GetProduct(int id);
    IEnumerable<Product> GetProducts();
    void InsertProduct(Product prod);
    void UpdateProduct(Product prod);
}