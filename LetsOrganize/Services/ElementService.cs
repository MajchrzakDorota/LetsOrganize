using LetsOrganize.Entities;

namespace LetsOrganize.Services
{
    public interface IElementService
    {
        Task<IEnumerable<Element>> GetAllElementFromList(int listId);
        Task<int> AddElementToList(int id, Element element);
        Task<bool> EditElement(int id, Element element);
        Task<bool> DeleteElementFromList(int elementId);
        Task<bool> DeleteAllElementsFromList(int id);
    }
    public class ElementService : IElementService
    {
        private readonly LetsOrganizeDbContext _context;
        public ElementService(LetsOrganizeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Element>> GetAllElementFromList(int listId)
        {
            var listOfElements = _context.Elements.Where(e => e.MyListId == listId).ToList();

            if(listOfElements == null)
            {
                throw new Exception("This list is empty");
            }

            return listOfElements;
        }

        public async Task<int> AddElementToList(int id, Element element)
        {
            var searchingList = _context.Lists.Where(l => l.Id == id);

            if(searchingList == null)
            {
                throw new Exception($"List with id: {id} Not Found");
            }

            var newElement = new Element()
            {
                MyListId = id,
                Name = element.Name,
                Unit = element.Unit,
                Quantity = element.Quantity
            };

            _context.Elements.Add(newElement);
            _context.SaveChanges();

            return newElement.Id;
            
        }
        public async Task<bool> EditElement(int id, Element element)
        {
            var elementToEdit = _context.Elements.FirstOrDefault(e => e.Id == id);

            if(elementToEdit == null)
            {
                throw new Exception("Element not found");
            }

            elementToEdit.Name = element.Name;

            _context.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteElementFromList(int elementId)
        {
            
            var element = _context.Elements.FirstOrDefault(e => e.Id == elementId);

            if(element == null)
            {
                throw new Exception($"Element with id: {elementId} not found");
            }

            _context.Elements.Remove(element);       
            _context.SaveChanges();
            return true;

        }

        public async Task<bool> DeleteAllElementsFromList(int id)
        {
            var searchingList = _context.Elements.Where(l => l.MyListId == id);

            if( searchingList == null)
            {
                throw new Exception("This list is empty");
            }
            _context.RemoveRange(searchingList);
            _context.SaveChanges();

            return true;
        }


    }
}
