using LetsOrganize.Entities;
using LetsOrganize.Interfaces;
using LetsOrganize.Models;
using LetsOrganize.Utils;
using Microsoft.EntityFrameworkCore;

namespace LetsOrganize.Services
{
    public class ElementService : IElementService
    {
        private readonly LetsOrganizeDbContext _context;
        private readonly ILogger<ElementService> _logger;

        public ElementService(LetsOrganizeDbContext context, ILogger<ElementService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<IEnumerable<ElementDto>> GetAllElementsFromList(int listId)
        {
            var listOfElementsDto = _context.Elements.Where(e => e.MyListId == listId).Select(element => DtoTransformer.CreateElementDto(element)).AsEnumerable();

            return Task.FromResult(listOfElementsDto);
        }

        public async Task<int> AddElementToList(int id, ElementDto elementDto)
        {
            var searchingList = _context.Lists.Where(l => l.Id == id);

            if (searchingList == null)
            {
                throw new NullReferenceException($"List with id: {id} Not Found");
            }

            var newElement = DtoTransformer.CreateElementFromElementDto(elementDto);

            _context.Elements.Add(newElement);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
            }

            return newElement.Id;
        }

        public async Task<bool> EditElement(int id, ElementDto elementDto)
        {
            var elementToEdit = _context.Elements.FirstOrDefault(e => e.Id == id);

            if (elementToEdit == null)
            {
                throw new NullReferenceException("Element not found");
            }

            elementToEdit.Name = elementDto.Name;
            elementToEdit.Unit = elementDto.Unit;
            elementToEdit.Quantity = elementDto.Quantity;
            elementToEdit.MyListId = elementDto.MyListId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
            }

            return true;
        }

        public async Task<bool> DeleteElementFromList(int elementId)
        {

            var element = _context.Elements.FirstOrDefault(e => e.Id == elementId);

            if (element == null)
            {
                throw new NullReferenceException($"Element with id: {elementId} not found");
            }

            _context.Elements.Remove(element);

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
            }

            return true;
        }

        public async Task<bool> DeleteAllElementsFromList(int id)
        {
            var searchingList = _context.Elements.Where(l => l.MyListId == id);

            if (searchingList == null)
            {
                throw new NullReferenceException($"List with id: {id} does not exist");
            }

            _context.RemoveRange(searchingList);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
            }

            return true;
        }
    }
}
