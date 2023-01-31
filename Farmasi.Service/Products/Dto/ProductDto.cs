using System;
using Farmasi.Core.Domain;

namespace Farmasi.Service.Products.Dto
{
    public class ProductDto : BaseDto
    {        
        public string Name { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}