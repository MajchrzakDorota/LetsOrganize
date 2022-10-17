
using LetsOrganize.Entities;
using LetsOrganize.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetsOrganize.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class MyListController : ControllerBase
    {
        private readonly IMyListService _myListService;


        public MyListController(IMyListService myListService)
        {
            _myListService = myListService;
        }

        [HttpGet("/lists")]
        public async Task<ActionResult> GetLists()
        {
            var lists = await _myListService.GetAllLists();
            return Ok(lists);
        }
        [HttpGet("/list/{id}")]
        public async Task<ActionResult> GetListById([FromRoute] int id)
        {
            var list = await _myListService.GetListById(id);
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]string name)
        {
            var newList = await _myListService.CreateList(name);

            return Ok(newList);
            //return CreatedAtAction(nameof(GetListById), name, newList);
        }
        [HttpPut("/list/{id}")]
        public async Task<ActionResult> ChangeListName([FromRoute] int id, [FromBody] string name)
        {
            var updatedList = await _myListService.ChangeListName(id, name);
            return Ok(updatedList);
        }
        [HttpDelete("/list/{id}")]
        public async Task<ActionResult> DeleteList([FromRoute] int id)
        {
            await _myListService.DeleteList(id);

            return Ok();
        }

    }
}
