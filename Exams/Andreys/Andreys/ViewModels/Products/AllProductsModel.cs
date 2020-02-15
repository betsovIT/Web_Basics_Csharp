using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.ViewModels.Products
{
    public class AllProductsModel
    {
        public ICollection<ProductInfo> Products { get; set; }
    }
}
