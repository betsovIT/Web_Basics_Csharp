using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Services
{
    public interface IProductsService
    {
        AllProductsModel GetAllProducts();

        ProductDetailsModel GetProduct(string id);

        void DeleteProduct(string id);

        void Add(ProductInputInfo input);
    }
}
