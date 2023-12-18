using _14_December.DAL;
using _14_December.Entities;
using _14_December.Repositories.Interfaces;
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
        private readonly IRepository _repository;

        public CategoriesController(AppDbContext context, IRepository repository)
        {
			_context = context;
			_repository = repository;
        }
		[HttpGet]
		public async Task<IActionResult> Get(int page=1, int take=3)
		{
			IEnumerable<Category> categories=await _repository.GetAllAsync();
			return Ok(categories);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			if (id <= 0)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			Category category= await _repository.GetByIDAsync(id);
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
		[HttpPut]
		public async Task<IActionResult> Update(int id,string name)
		{
			if (id <= 0)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			Category category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);
			if (category == null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			category.Name = name;
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
			Category category= await _context.Categories.FirstOrDefaultAsync(c=> c.Id==id);
			if (category == null)
			{
				return StatusCode(StatusCodes.Status404NotFound);
			}
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			return NoContent();
		}
    }
}
