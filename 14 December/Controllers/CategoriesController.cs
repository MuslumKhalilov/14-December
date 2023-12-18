﻿using _14_December.DAL;
using _14_December.Dtos;
using _14_December.Entities;
using _14_December.Repositories.Interfaces;
using _14_December.Services.Interfaces;
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
        private readonly ICategoryRepository _repository;
        private readonly ICategoryService _service;

        public CategoriesController(AppDbContext context, ICategoryRepository repository,ICategoryService service)
        {
			_context = context;
			_repository = repository;
			_service= service;
        }
		[HttpGet]
		public async Task<IActionResult> Get(int page=1, int take=3)
		{
			
			return Ok(await _service.GetAllAsync(page,take));
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			if (id <= 0)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}
			
			
			return Ok(_service.GetByIdAsync(id));
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromForm]CreateCategoryDto categoryDto)
		{
			await _service.CreateAsync(categoryDto);
			return StatusCode(StatusCodes.Status201Created);
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
			_repository.Update(category);
			await _repository.SaveChangesAsync();
			return NoContent();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
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

			_repository.DeleteAsync(category);
			await _repository.SaveChangesAsync();
			return NoContent();
		}
    }
}
