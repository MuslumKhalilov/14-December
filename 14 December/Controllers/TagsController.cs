using _14_December.DAL;
using _14_December.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _14_December.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TagsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public TagsController(AppDbContext context)
        {
            _context = context;
        }
		[HttpGet]
		public async Task<IActionResult> Get(int page = 1, int take = 3)
		{
			List<Tag> tags = await _context.Tags.Skip((page - 1) * take).Take(take).ToListAsync();
			return Ok(tags);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			if (id <= 0)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			Tag tag = await _context.Tags.FirstOrDefaultAsync(c => c.Id == id);
			if (tag == null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			return Ok(tag);
		}
		[HttpPost]
		public async Task<IActionResult> Create(Tag tag)
		{
			_context.Tags.Add(tag);
			await _context.SaveChangesAsync();
			return StatusCode(StatusCodes.Status201Created, tag);
		}
		[HttpPut]
		public async Task<IActionResult> Update(int id, string name)
		{
			if (id <= 0)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			Tag tag = await _context.Tags.FirstOrDefaultAsync(c => c.Id == id);
			if (tag == null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			tag.Name = name;
			await _context.SaveChangesAsync();
			return NoContent();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			Tag tag = await _context.Tags.FirstOrDefaultAsync(c => c.Id == id);
			if (tag == null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			_context.Tags.Remove(tag);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}

