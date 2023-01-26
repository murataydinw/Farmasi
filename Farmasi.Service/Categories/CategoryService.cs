using System;
using Farmasi.Data.Categories;

namespace Farmasi.Service.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _category;
        public CategoryService(ICategoryRepository category)
        {
            _category = category;
        }
        public object GetDetail(string name, string url)
        {
            return _category.Get(x => x.Url == url);
        }

        public List<object> GetList(int skip, int take = 10)
        {
            throw new NotImplementedException();
        }
    }
}

