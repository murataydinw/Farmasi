using System;
using System.Xml.Linq;
using Farmasi.Core.Domain.Categories;
using Farmasi.Data.Categories;
using Farmasi.Service.Categories.Dto;
using Farmasi.Service.Categories.Input;

namespace Farmasi.Service.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _category;
        public CategoryService(ICategoryRepository category)
        {
            _category = category;
        }       

        public CategoryDto GetDetail(CategoryDetailParameter input)
        {
            var model = _category.Get(x => x.Url == input.Url);
            return model == null ? new CategoryDto() : new CategoryDto
            {
                Id = model.Id,
                Name = model.Name,
                Url = model.Url

            };
        }

        public CategoryDto Save(CategoryDto input)
        {
            var data = new Category { Name = input.Name, Url = input.Url };
            _category.Create(data);
            input.Id = data.Id;
            return input;
        }

        public List<CategoryDto> GetList(CategoryListParameter input)
        {
            return _category.GetList(x => true).Skip(input.Skip).Take(input.Take)
                .Select(x => new CategoryDto { Name = x.Name, Url = x.Url, Id = x.Id }).ToList();
        }
    }
}

