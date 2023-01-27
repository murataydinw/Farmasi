using System;
namespace Farmasi.Service.Products.Input
{
	public class ProductListParameter
	{       
        public string CategoryName { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}

