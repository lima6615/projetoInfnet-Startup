using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using StartupProject.Dtos;
using StartupProject.Service.Interface;

namespace StartupProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _service;

        public QuizController(IQuizService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuizDTO>>> GetAllAsync()
        {
            var dtos = await _service.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("{id:guid}", Name = "GetQuizById")]
        public async Task<ActionResult<QuizDTO>> GetByIdAsync(Guid id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<QuizDTO>> CreateAsync([FromBody] QuizDTO dto)
        {
            if (dto is null) return BadRequest();
            var created = await _service.CreateAsync(dto);
            return CreatedAtRoute("GetQuizById", new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<QuizDTO>> UpdateAsync(Guid id, [FromBody] QuizDTO dto)
        {
            if (dto is null) return BadRequest();
            if (id != dto.Id) return BadRequest("Id mismatch.");

            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();

            var updated = await _service.GetByIdAsync(id);
            if (updated is null) return NotFound();

            return Ok(updated);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<QuizDTO>> PatchAsync(Guid id, [FromBody] JsonElement patchData)
        {
            if (patchData.ValueKind != JsonValueKind.Object) return BadRequest("Payload must be a JSON object.");

            var ok = await _service.PatchAsync(id, patchData);
            if (!ok) return NotFound();

            var updated = await _service.GetByIdAsync(id);
            if (updated is null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}