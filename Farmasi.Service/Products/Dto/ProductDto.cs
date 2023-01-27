using System;
namespace Farmasi.Service.Products.Dto
{
	public class ProductDto
	{
        public Guid? Uid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}

