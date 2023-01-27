using System;
namespace Farmasi.Service.Categories.Input
{
	public class CategoryListParameter
	{
		public int Skip { get; set; }
		public int Take { get; set; } = 10;
    }
}

