using WebApplication10.Models;

namespace WebApplication10
{
    public class ProductService
    {
        public List<Product> GetProducts() {
            return new List<Product>()
            {
                new Product() {
                    Id = 1,
                    Name = "Auto",
                    Price = 800000
                },
                new Product()
                {
                    Id = 2,
                    Name = "Kolo",
                    Price = 30000
                },
                new Product()
                {
                    Id = 3,
                    Name = "Brusle",
                    Price = 3000
                }
            };
        }


        public Product GetProduct(int id) {
            return GetProducts().FirstOrDefault(x => x.Id == id);
        }

    }
}
