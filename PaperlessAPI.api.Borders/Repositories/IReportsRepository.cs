using PaperlessAPI.api.Borders.Dtos;
using PaperlessAPI.api.Borders.Entities;

namespace PaperlessAPI.api.Borders.Repositories
{
    public interface IReportsRepository
    {
        Task<DynamicReportEntityDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<DynamicReportEntity>> GetAllAsync();
        Task<DynamicReportEntityDto?> InsertAsync(DynamicReportEntity report);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}