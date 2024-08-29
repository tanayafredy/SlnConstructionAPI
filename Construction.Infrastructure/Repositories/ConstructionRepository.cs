using Construction.Application.Interfaces;
using Construction.Domain.Entities;
using Construction.Infrastructure.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using System.Text;

namespace Construction.Infrastructure.Repositories
{
    public class ConstructionRepository : IConstructionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;

        public ConstructionRepository(ApplicationDbContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetDbConnection().ConnectionString;
        }

        public async Task<IEnumerable<ConstructionProject>> GetAllAsync(string filter = null, string sortField = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var query = new StringBuilder("SELECT * FROM GetAllConstructions()");

                if (!string.IsNullOrEmpty(filter))
                {
                    query.Append($" WHERE ProjectName ILIKE '%{filter}%' OR ProjectDetail ILIKE '%{filter}%'");
                }

                if (!string.IsNullOrEmpty(sortField))
                {
                    query.Append($" ORDER BY {sortField} {(ascending ? "ASC" : "DESC")}");
                }

                query.Append($" LIMIT {pageSize} OFFSET {(pageNumber - 1) * pageSize}");

                return await connection.QueryAsync<ConstructionProject>(query.ToString());
            }
        }

        public async Task<ConstructionProject> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("_ProjectID", id, DbType.Int32);

                return await connection.QueryFirstOrDefaultAsync<ConstructionProject>("SELECT * FROM GetConstructionById(@_ProjectID)", parameters);
            }
        }

        public async Task AddAsync(ConstructionProject constructionProject)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("_ProjectName", constructionProject.ProjectName, DbType.String);
                parameters.Add("_ProjectLocation", constructionProject.ProjectLocation, DbType.String);
                parameters.Add("_ProjectStage", constructionProject.ProjectStage, DbType.String);
                parameters.Add("_ProjectCategory", constructionProject.ProjectCategory, DbType.String);
                parameters.Add("_ProjectConstructionStartDate", constructionProject.ProjectConstructionStartDate, DbType.Date);
                parameters.Add("_ProjectDetail", constructionProject.ProjectDetail, DbType.String);
                parameters.Add("_ProjectCreatorID", constructionProject.ProjectCreatorID, DbType.String);
                parameters.Add("_ProjectID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("CALL InsertConstruction(@_ProjectName, @_ProjectLocation, @_ProjectStage, @_ProjectCategory,@_ProjectConstructionStartDate,@_ProjectDetail,@_ProjectCreatorID,0)", parameters);

                constructionProject.ProjectID = parameters.Get<int>("_ProjectID");
            }
        }

        public async Task UpdateAsync(ConstructionProject constructionProject)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("_ProjectID", constructionProject.ProjectID, DbType.Int32);
                parameters.Add("_ProjectName", constructionProject.ProjectName, DbType.String);
                parameters.Add("_ProjectLocation", constructionProject.ProjectLocation, DbType.String);
                parameters.Add("_ProjectStage", constructionProject.ProjectStage, DbType.String);
                parameters.Add("_ProjectCategory", constructionProject.ProjectCategory, DbType.String);
                parameters.Add("_ProjectConstructionStartDate", constructionProject.ProjectConstructionStartDate, DbType.Date);
                parameters.Add("_ProjectDetail", constructionProject.ProjectDetail, DbType.String);
                parameters.Add("_ProjectCreatorID", constructionProject.ProjectCreatorID, DbType.String);

                await connection.ExecuteAsync("CALL UpdateConstruction(@_ProjectID,@_ProjectName, @_ProjectLocation, @_ProjectStage, @_ProjectCategory,@_ProjectConstructionStartDate,@_ProjectDetail,@_ProjectCreatorID)", parameters);

            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("_ProjectID", id, DbType.Int32);

                await connection.ExecuteAsync("CALL DeleteConstruction(@_ProjectID)", parameters);
            }
        }
    }
}
