using LetsOrganize.Entities;
using LetsOrganize.Interfaces;
using LetsOrganize.Models;
using LetsOrganize.Utils;
using Microsoft.EntityFrameworkCore;

namespace LetsOrganize.Services
{
    public class MyListService : IMyListService
    {
        private readonly LetsOrganizeDbContext _context;
        private readonly ILogger<MyListService> _logger;

        public MyListService(LetsOrganizeDbContext context, ILogger<MyListService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<MyListDto>> GetAllLists()
        {
            var lists = _context.Lists.Select(list => DtoTransformer.CreateMyListDto(list)).ToList();

            if (lists == null)
            {
                throw new NullReferenceException("Any list not found");
            }

            return await Task.FromResult(lists);
        }

        public async Task<MyListDto> GetListById(int id)
        {
            var list = _context.Lists.FirstOrDefault(l => l.Id == id);

            if (list == null)
            {
                throw new NullReferenceException($"List with id: {id} not found.");
            }

            var myListDto = DtoTransformer.CreateMyListDto(list);

            return await Task.FromResult(myListDto);
        }

        public async Task<int> CreateList(MyListDto newListDto)
        {
            var newCreatedListDto = new MyListDto()
            {
                Id = newListDto.Id,
                Name = newListDto.Name
            };

            var newCreatedList = DtoTransformer.CreateMyListFromMyListDto(newCreatedListDto);

            _context.Lists.Add(newCreatedList);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
            }

            return newCreatedList.Id;
        }

        public async Task<bool> EditList(int id, MyListDto newListDto)
        {
            var searchingList = _context.Lists.FirstOrDefault(l => l.Id == id);

            if (searchingList == null)
            {
                throw new NullReferenceException($"List with id: {id} not found.");
            }

            searchingList.Id = newListDto.Id;
            searchingList.Name = newListDto.Name;

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

        public async Task<bool> DeleteList(int id)
        {
            var searchingList = _context.Lists.FirstOrDefault(l => l.Id == id);

            if (searchingList == null)
            {
                throw new NullReferenceException($"List with id: {id} not found.");
            }

            _context.Lists.Remove(searchingList);

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
