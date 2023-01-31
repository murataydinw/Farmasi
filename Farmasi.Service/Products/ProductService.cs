using System;
using Farmasi.Core.Domain.Categories;
using System.Xml.Linq;
using Farmasi.Service.Products.Dto;
using Farmasi.Service.Products.Input;
using Farmasi.Core.Domain.Produtcs;
using Farmasi.Data.Categories;
using Farmasi.Data.Products;
using LinqKit;

namespace Farmasi.Service.Products
{
    public class ProductService : IProductService
    {
        private readonly ICategoryRepository _category;
        private readonly IProductRepository _product;

        public ProductService(ICategoryRepository category, IProductRepository product)
        {
            _category = category;
            _product = product;
        }

        public ProductDto GetDetail(ProductDetailParameter input)
        {
            var predicate = PredicateBuilder.New<Product>();
            if (!string.IsNullOrEmpty(input.Name))
            {
                predicate = predicate.And(x => x.Name == input.Name);
            }
            if (!string.IsNullOrEmpty(input.Url))
            {
                predicate = predicate.And(x => x.Url == input.Url);
            }

            var result = _product.Get(predicate);
            return new ProductDto { Name = result.Name, Url = result.Url, Price = result.Price, Id = result.Id };
        }

        public List<ProductDto> GetList(ProductListParameter input)
        {
            var category = _category.Get(x => x.Name == input.CategoryName);
            var predicate = PredicateBuilder.New<Product>();
            if (category != null)
            {
                predicate = predicate.And(x => x.CategoryId == category.Id);
            }
            else
            {
                predicate = predicate.And(x => true);
            }

            var products = _product.GetList(predicate).Skip(input.Skip).Take(input.Take);
            var categories = _category.GetList(x => true);

            return (from p in products
                        join c in categories on p.CategoryId equals c.Id
                        select new ProductDto
                        {
                            Name = p.Name,
                            Url = p.Url,
                            Price = p.Price,
                            Category=c.Name,
                            Id = p.Id
                        }).ToList();          
            //Burada result gonderirken automapper kullanmak daha mantikli fakat case cok kucuk oldugu icin kullanma gereksinimi duymadim.
        }

        public ProductDto Save(ProductDto input)
        {
            var category = _category.Get(x => x.Name.ToLower() == input.Category.ToLower());
            if (category == null) return new ProductDto();

            var product = new Product { Name = input.Name, Url = input.Url, CreatedAt = DateTime.Now, CategoryId = category.Id, Price = input.Price };
            _product.Create(product);
            return input;
        }

    }
}

