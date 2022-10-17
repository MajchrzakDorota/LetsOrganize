using LetsOrganize.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsOrganize.Services
{
    public interface IMyListService
    {
        Task<IEnumerable<MyList>> GetAllLists();
        Task<MyList> GetListById(int id);
        Task<int> CreateList(string name);
        Task<bool> ChangeListName(int id, string name);
        Task<bool> DeleteList(int id);

    }
    public class MyListService : IMyListService
    {
        private readonly LetsOrganizeDbContext _context;
        public MyListService(LetsOrganizeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MyList>> GetAllLists()
        {
            var lists = _context.Lists.ToList();
            if(lists == null)
            {
                throw new Exception("Any list not found");
            }
            return lists;

        }
        public async Task<MyList> GetListById(int id)
        {
            var list = _context.Lists.FirstOrDefault(l => l.Id == id);

            if(list == null)
            {
                throw new Exception($"List with id: {id} not found.");
            }

            return list;
        }
        public async Task<int> CreateList(string name)
        {
            var newList = new MyList()
            {
                Name = name
            };
       

            _context.Lists.Add(newList);
            _context.SaveChanges();

            return newList.Id;
        }

        public async Task<bool> ChangeListName(int id, string name)
        {
            var searchingList = _context.Lists.FirstOrDefault(l => l.Id == id);

            if (searchingList == null)
            {
                throw new Exception($"List with id: {id} not found.");
            }
                
            searchingList.Name = name;

            _context.SaveChanges();

            return true;

        }

        public async Task<bool> DeleteList (int id)
        {
            var searchingList = _context.Lists.FirstOrDefault(l => l.Id == id);

            if (searchingList == null)
            {
                throw new Exception($"List with id: {id} not found.");
            }
            _context.Lists.Remove(searchingList);
            _context.SaveChanges();

            return true;

        }
    }
}
