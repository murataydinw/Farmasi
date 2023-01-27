using System;
using Farmasi.Service.Products.Dto;
using Farmasi.Service.Products.Input;

namespace Farmasi.Service.Products
{
	public interface IProductService
	{
        ProductDto GetDetail(ProductDetailParameter input);
        List<ProductDto> GetList(ProductListParameter input);
        ProductDto Save(ProductDto input);
    }
}

