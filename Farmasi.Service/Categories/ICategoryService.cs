using Farmasi.Service.Categories.Dto;
using Farmasi.Service.Categories.Input;

namespace Farmasi.Service.Categories
{
    public interface ICategoryService
    {
        CategoryDto GetDetail(CategoryDetailParameter input);
        CategoryDto Save(CategoryDto input);
        List<CategoryDto> GetList(CategoryListParameter input);
    }
}