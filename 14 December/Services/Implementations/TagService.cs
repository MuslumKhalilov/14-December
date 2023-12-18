using _14_December.Dtos;
using _14_December.Entities;
using _14_December.Repositories.Interfaces;
using _14_December.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _14_December.Services.Implementations
{
    public class TagService:ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateTagDto categoryDto)
        {
            await _repository.AddAsync(new Tag { Name = categoryDto.Name });
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag category = await _repository.GetByIDAsync(id);
            if (category == null) throw new Exception("Not found");
            _repository.DeleteAsync(category);
            await _repository.SaveChangesAsync();

        }

        public async Task<ICollection<GetTagDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();
            ICollection<GetTagDto> getTagDtos = new List<GetTagDto>();
            foreach (var tag in tags)
            {
                getTagDtos.Add(new GetTagDto { Id = tag.Id, Name = tag.Name, });
            }
            return getTagDtos;
        }

        public async Task<GetTagDto> GetByIdAsync(int id)
        {
            Tag tag = await _repository.GetByIDAsync(id);
            if (tag is null) throw new Exception("NotFound");
            GetTagDto dto = new GetTagDto { Name = tag.Name, Id = tag.Id };
            return dto;
        }

        public async Task UpdateAsync(int id, string name)
        {
            Tag tag = await _repository.GetByIDAsync(id);
            if (tag is null) throw new Exception("Not found");
            tag.Name = name;
            await _repository.SaveChangesAsync();
        }
    }
}
