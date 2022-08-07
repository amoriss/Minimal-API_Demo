using MinimalAPI_Demo;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IDbConnection>((s) =>
{
    IDbConnection conn = new MySqlConnection(config.GetConnectionString("bestbuy"));
    conn.Open();
    return conn;
});

var app = builder.Build();

app.MapGet("/", () => "Hello Mock Bestbuy API!");
app.MapGet("/GetAllProducts", (IProductRepo productRepo) => productRepo.GetProducts());

app.MapPost("/InsertProduct", (IProductRepo productRepo, Product product) =>
{
    int lastProductId = productRepo.GetProducts().LastOrDefault().ProductID;
    product.ProductID = ++lastProductId;
    productRepo.InsertProduct(product);
});

app.MapPut("/UpdateProduct/{id:int}", (IProductRepo productRepo, Product inputProduct, int id) =>
{
    var productToUpdate = productRepo.GetProduct(id);
    productToUpdate.Name = inputProduct.Name;
    productToUpdate.Price = inputProduct.Price;
    productToUpdate.CategoryID = inputProduct.CategoryID;
    productToUpdate.OnSale = inputProduct.OnSale;
    productToUpdate.StockLevel = inputProduct.StockLevel;
    productRepo.UpdateProduct(productToUpdate);
});

app.MapDelete("/DeleteProduct/{id:int}", (IProductRepo productRepo, int id) =>
{
    productRepo.DeleteProduct(id);
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();
