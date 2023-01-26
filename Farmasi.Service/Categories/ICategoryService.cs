namespace Farmasi.Service.Categories
{
    public interface ICategoryService
    {
        object GetDetail(string name="", string url="");
        object Save(string name, string url);
        List<object> GetList(int skip, int take = 10);
    }
}