using Andreys.Data;
using Andreys.Models;
using Andreys.Models.Enums;
using Andreys.ViewModels.Products;
using System;
using System.Linq;


namespace Andreys.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void Add(ProductInputInfo input)
        {
            var product = new Product()
            {
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                Price = input.Price,
                Category = Enum.Parse<Category>(input.Category),
                Gender = Enum.Parse<Gender>(input.Gender)
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public void DeleteProduct(string id)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == int.Parse(id));

            db.Products.Remove(product);
            db.SaveChanges();
        }

        public AllProductsModel GetAllProducts()
        {
            var products = db.Products.Select(p => new ProductInfo
            {
                Id = p.Id,
                Name = p.Name,
                ImgUrl = p.ImageUrl,
                Price = p.Price
            }).ToList();

            var model = new AllProductsModel();

            model.Products = products;

            return model;
        }

        public ProductDetailsModel GetProduct(string id)
        {
            int IdAsInt = int.Parse(id);

            var result = db.Products.Where(p => p.Id == IdAsInt)?.Select(p => new ProductDetailsModel
            {
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Gender = p.Gender.ToString(),
                Category = p.Category.ToString()
            }).FirstOrDefault(); 

            return result;
        }
    }
}
