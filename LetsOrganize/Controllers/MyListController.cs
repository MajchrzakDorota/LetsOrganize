using LetsOrganize.Interfaces;
using LetsOrganize.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetsOrganize.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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

        [HttpGet("/lists/{id}")]
        public async Task<ActionResult> GetListById([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be a positive number.");
            }

            var list = await _myListService.GetListById(id);

            return Ok(list);
        }

        [HttpPost("/lists")]
        public async Task<ActionResult> Create([FromBody] MyListDto newListDto)
        {
            if (newListDto == null)
            {
                return BadRequest("Incorrect data entered");
            }

            var newCreatedList = await _myListService.CreateList(newListDto);

            return Created($"/lists/{newCreatedList}", null);
        }

        [HttpPut("/lists/{id}")]
        public async Task<ActionResult> EditList([FromRoute] int id, [FromBody] MyListDto newListDto)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be a positive number.");
            }

            if (newListDto == null)
            {
                return BadRequest("Incorrect data entered");
            }

            var updatedList = await _myListService.EditList(id, newListDto);
            
            return Ok(updatedList);
        }

        [HttpDelete("/lists/{id}")]
        public async Task<ActionResult> DeleteList([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be a positive number.");
            }

            await _myListService.DeleteList(id);

            return Ok();
        }

    }
}
