using System;
using Farmasi.Core.Domain.Categories;
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
            var data = new Category { Name = "Biskuvi", Url = "biskuvi" };

            return _category.Get(x => x.Url == url);
        }
        public object Save(string name, string url)
        {
            var data = new Category { Name = name, Url = url };

          var result=  _category.Create(data);

            return result;

        }

        public List<object> GetList(int skip, int take = 10)
        {
            throw new NotImplementedException();
        }
    }
}

