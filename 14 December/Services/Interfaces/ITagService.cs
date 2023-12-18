using _14_December.Dtos;

namespace _14_December.Services.Interfaces
{
    public interface ITagService
    {
        Task<ICollection<GetTagDto>> GetAllAsync(int page, int take);
        Task<GetTagDto> GetByIdAsync(int id);
        Task CreateAsync(CreateTagDto categoryDto);
        Task UpdateAsync(int id, string name);
        Task DeleteAsync(int id);
    }
}
