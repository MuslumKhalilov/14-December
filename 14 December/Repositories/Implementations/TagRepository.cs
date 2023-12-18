using _14_December.DAL;
using _14_December.Entities;
using _14_December.Repositories.Interfaces;

namespace _14_December.Repositories.Implementations
{
    public class TagRepository:Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
