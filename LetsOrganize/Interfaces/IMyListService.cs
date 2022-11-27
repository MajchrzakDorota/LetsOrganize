using LetsOrganize.Models;

namespace LetsOrganize.Interfaces
{
    public interface IMyListService
    {
        Task<IEnumerable<MyListDto>> GetAllLists();
        Task<MyListDto> GetListById(int id);
        Task<int> CreateList(MyListDto myListDto);
        Task<bool> EditList(int id, MyListDto newListDto);
        Task<bool> DeleteList(int id);
    }
}
