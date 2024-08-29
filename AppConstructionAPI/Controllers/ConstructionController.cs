using Construction.Application.Services;
using Construction.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppConstructionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionController : Controller
    {
        private readonly ConstructionService _constructionService;

        public ConstructionController(ConstructionService constructionService)
        {
            _constructionService = constructionService;
        }

        // GET: api/students
        [HttpGet]
        public async Task<IEnumerable<ConstructionProject>> GetConstructions(
            [FromQuery] string filter = null,
            [FromQuery] string sortField = null,
            [FromQuery] bool ascending = true,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            return await _constructionService.GetAllConstructionsAsync(filter, sortField, ascending, pageNumber, pageSize);
        }

        // GET: api/students/{id}
        [HttpGet("{ProjectID}")]
        public async Task<ActionResult<ConstructionProject>> GetConstruction(int id)
        {
            var student = await _constructionService.GetConstructionByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public async Task<ActionResult<ConstructionProject>> PostConstructionProject(ConstructionProject constructionProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (constructionProject.ProjectStage == "Concept" || constructionProject.ProjectStage == "Design & Documentation" || constructionProject.ProjectStage == "Pre-Construction")
            {
                if (constructionProject.ProjectConstructionStartDate.Date <= DateTime.Now.Date)
                {

                    return BadRequest("Construction Start Date must be future date");
                }
            }
            await _constructionService.AddConstructionAsync(constructionProject);
            return CreatedAtAction("GetConstruction", new { ProjectID = constructionProject.ProjectID }, constructionProject);
        }

        [HttpPut("{ProjectID}")]
        public async Task<IActionResult> PutConstruction(int id, ConstructionProject constructionProject)
        {
            if (id != constructionProject.ProjectID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (constructionProject.ProjectStage == "Concept" || constructionProject.ProjectStage == "Design & Documentation" || constructionProject.ProjectStage == "Pre-Construction")
            {
                if (constructionProject.ProjectConstructionStartDate.Date <= DateTime.Now.Date)
                {

                    return BadRequest("Construction Start Date must be future date");
                }
            }
            await _constructionService.UpdateConstructionAsync(constructionProject);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConstruction(int id)
        {
            await _constructionService.DeleteConstructionAsync(id);
            return NoContent();
        }
    }
}
