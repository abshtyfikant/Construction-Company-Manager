using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IMaterialRepository
{
    void DeleteMaterial(int materialId);
    int AddMaterial(Material material);
    IQueryable<Material> GetAllMaterials();
    IQueryable<Material> GetMaterialsByService(int serviceId);
    Material GetMaterial(int materialId);
    void UpdateMaterial(Material material);
    double getTotalCostInTimeRange(DateTime startDate, DateTime endDate);
}
