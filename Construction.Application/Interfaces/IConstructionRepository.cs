using Construction.Domain.Entities;

namespace Construction.Application.Interfaces
{
    public interface IConstructionRepository
    {
        Task<IEnumerable<ConstructionProject>> GetAllAsync(string filter = null, string sortField = null, bool ascending = true, int pageNumber = 1, int pageSize = 10);
        Task<ConstructionProject> GetByIdAsync(int id);
        Task AddAsync(ConstructionProject constructionProject);
        Task UpdateAsync(ConstructionProject constructionProject);
        Task DeleteAsync(int id);

    }
}
