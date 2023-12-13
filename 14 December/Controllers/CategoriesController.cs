using _14_December.DAL;
using _14_December.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _14_December.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CategoriesController(AppDbContext context)
        {
            _context = context;
        }
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			List<Category> categories=await _context.Categories.ToListAsync();
			return Ok(categories);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			if (id <= 0)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			Category category= await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);
			if (category == null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			return Ok(category);
		}
		[HttpPost]
		public async Task<IActionResult> Create(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return StatusCode(StatusCodes.Status201Created,category);
		}
    }
}
