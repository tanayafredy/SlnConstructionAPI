using Construction.Application.Interfaces;
using Construction.Domain.Entities;

namespace Construction.Application.Services
{
    public class ConstructionService
    {
        private readonly IConstructionRepository _constructionRepository;

        public ConstructionService(IConstructionRepository constructionRepository)
        {
            _constructionRepository = constructionRepository;
        }

        public async Task<IEnumerable<ConstructionProject>> GetAllConstructionsAsync(string filter = null, string sortField = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            return await _constructionRepository.GetAllAsync(filter, sortField, ascending, pageNumber, pageSize);
        }

        public async Task<ConstructionProject> GetConstructionByIdAsync(int id)
        {
            return await _constructionRepository.GetByIdAsync(id);
        }

        public async Task AddConstructionAsync(ConstructionProject constructionProject)
        {
            await _constructionRepository.AddAsync(constructionProject);
        }

        public async Task UpdateConstructionAsync(ConstructionProject constructionProject)
        {
            await _constructionRepository.UpdateAsync(constructionProject);
        }

        public async Task DeleteConstructionAsync(int id)
        {
            await _constructionRepository.DeleteAsync(id);
        }
    }
}

