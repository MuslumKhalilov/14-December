using _14_December.Dtos;
using _14_December.Entities;
using _14_December.Repositories.Interfaces;
using _14_December.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace _14_December.Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            await _repository.AddAsync(new Category {Name = categoryDto.Name });
        }

        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip:(page-1)*take,take:take,isTracking:false).ToListAsync();
            ICollection<GetCategoryDto> getCategoryDtos = new List<GetCategoryDto>();
            foreach (var category in categories)
            {
                getCategoryDtos.Add(new GetCategoryDto { Id = category.Id, Name = category.Name, });
            }
            return getCategoryDtos;
        }

        public async Task<GetCategoryDto> GetByIdAsync(int id)
        {
            Category category = await _repository.GetByIDAsync(id);
            if (category is null) throw new Exception("NotFound");
            GetCategoryDto dto = new GetCategoryDto { Name = category.Name, Id = category.Id };
            return dto;
        }
    }
}
