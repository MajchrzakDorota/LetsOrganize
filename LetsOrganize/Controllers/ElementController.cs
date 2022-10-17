using LetsOrganize.Entities;
using LetsOrganize.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetsOrganize.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var listOfElements = await _elementService.GetAllElementFromList(id);

            return Ok(listOfElements);
        }

        [HttpPost("/elements/{id}")]
        public async Task<ActionResult> AddElementToList([FromRoute] int id, [FromBody] Element element)
        {
            var listToAddElement = await _elementService.AddElementToList(id, element);
            
            return Ok(listToAddElement);


        }
        [HttpPut("/elements/{ElementId}")]
        public async Task<ActionResult> EditElement([FromRoute] int ElementId, [FromBody] Element element)
        {
            var editedElement = await _elementService.EditElement(ElementId, element);

            return Ok(editedElement);


        }

        [HttpDelete("/elements/all/{id}")]
        public async Task<ActionResult> DeleteAllElements([FromRoute] int id)
        {
            await _elementService.DeleteAllElementsFromList(id);

            return Ok();
        }

        [HttpDelete("/elements/{elementId}")]
        public async Task<ActionResult> DeleteElement([FromRoute] int elementId)
        {
            await _elementService.DeleteElementFromList(elementId);

            return Ok();
        }
    }
}
