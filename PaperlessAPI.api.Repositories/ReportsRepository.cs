using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PaperlessAPI.api.Borders.Database;
using PaperlessAPI.api.Borders.Dtos;
using PaperlessAPI.api.Borders.Entities;
using PaperlessAPI.api.Borders.Repositories;
using PaperlessAPI.api.Repositories.Database;
using PaperlessAPI.api.Shared.Properties;

namespace PaperlessAPI.api.Repositories
{
    public class ReportsRepository(
    IDatabaseFactory databaseFactory,
    ILogger<ReportsRepository> logger)
    : DatabaseRepository<DynamicReportEntity, Guid>(databaseFactory, logger), IReportsRepository
    {
        public async Task<DynamicReportEntityDto?> InsertAsync(DynamicReportEntity report)
        {
            try
            {
                await _collection.InsertOneAsync(report);
                return await GetByIdAsync(report.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir relatório.");
                _logger.LogError(ex, "{ErrorMessage}", Resources.CreateReportError);
                return null;
            }
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var filter = Builders<DynamicReportEntity>.Filter.Eq(m => m.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<DynamicReportEntityDto?> GetByIdAsync(Guid id)
        {
            var filter = Builders<DynamicReportEntity>.Filter.Eq(m => m.Id, id);
            var entity = await _collection.Find(filter).FirstOrDefaultAsync();

            return entity is not null ? ToDto(entity) : null;
        }

        private static DynamicReportEntityDto ToDto(DynamicReportEntity report)
        {
            return new DynamicReportEntityDto(
                report.Id,
                report.Title,
                report.ColumnHeaders,
                report.Rows,
                report.CreatedAt
            );
        }
    }
}