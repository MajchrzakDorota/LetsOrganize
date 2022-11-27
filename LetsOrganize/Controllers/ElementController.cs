using LetsOrganize.Interfaces;
using LetsOrganize.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetsOrganize.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ElementController : ControllerBase
    {
        private readonly IElementService _elementService;

        public ElementController(IElementService elementService)
        {
            _elementService = elementService;
        }

        [HttpGet("/elements/{id}")]
        public async Task<ActionResult> GetAllElmentsFromList([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be a positive number.");
            }

            var listOfElements = await _elementService.GetAllElementsFromList(id);

            return Ok(listOfElements);
        }

        [HttpPost("/elements/{id}")]
        public async Task<ActionResult> AddElementToList([FromRoute] int id, [FromBody] ElementDto elementDto)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be a positive number.");
            }

            if (elementDto == null)
            {
                return BadRequest("Incorrect data entered");
            }

            var listToAddElement = await _elementService.AddElementToList(id, elementDto);

            return Ok(listToAddElement);
        }

        [HttpPut("/elements/{elementId}")]
        public async Task<ActionResult> EditElement([FromRoute] int elementId, [FromBody] ElementDto elementDto)
        {
            if (elementId <= 0)
            {
                return BadRequest("ElementId must be a positive number.");
            }

            var editedElement = await _elementService.EditElement(elementId, elementDto);

            return Ok(editedElement);
        }

        [HttpDelete("/elements/all/{id}")]
        public async Task<ActionResult> DeleteAllElements([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be a positive number.");
            }

            await _elementService.DeleteAllElementsFromList(id);

            return Ok();
        }

        [HttpDelete("/elements/{elementId}")]
        public async Task<ActionResult> DeleteElement([FromRoute] int elementId)
        {

            if (elementId <= 0)
            {
                return BadRequest("ElementId must be a positive number.");
            }

            await _elementService.DeleteElementFromList(elementId);

            return Ok();
        }
    }
}
