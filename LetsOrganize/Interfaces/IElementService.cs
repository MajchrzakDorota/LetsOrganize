using LetsOrganize.Models;

namespace LetsOrganize.Interfaces
{
    public interface IElementService
    {
        Task<IEnumerable<ElementDto>> GetAllElementsFromList(int listId);
        Task<int> AddElementToList(int id, ElementDto elementDto);
        Task<bool> EditElement(int id, ElementDto elementDto);
        Task<bool> DeleteElementFromList(int elementId);
        Task<bool> DeleteAllElementsFromList(int id);
    }
}
