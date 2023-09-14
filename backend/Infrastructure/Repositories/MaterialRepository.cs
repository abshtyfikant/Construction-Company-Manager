using Domain.Interfaces.Repository;
using Domain.Model;

namespace Infrastructure.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MaterialRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int AddMaterial(Material material)
    {
        if (!_dbContext.Services.Any(s => s.Id == material.ServiceId)) throw new Exception("Service not found");

        _dbContext.Materials.Add(material);
        _dbContext.SaveChanges();
        return material.Id;
    }

    public void DeleteMaterial(int materialId)
    {
        var material = _dbContext.Materials.Find(materialId);
        if (material is not null)
        {
            _dbContext.Materials.Remove(material);
            _dbContext.SaveChanges();
        }
    }

    public IQueryable<Material> GetAllMaterials()
    {
        var materials = _dbContext.Materials;
        return materials;
    }

    public IQueryable<Material> GetMaterialsByService(int serviceId)
    {
        if (!_dbContext.Services.Any(s => s.Id == serviceId)) throw new Exception("Service not found");

        var materials = _dbContext.Materials.Where(m => m.ServiceId == serviceId);
        return materials;
    }

    public Material GetMaterial(int materialId)
    {
        var material = _dbContext.Materials.FirstOrDefault(i => i.Id == materialId);
        return material;
    }

    public void UpdateMaterial(Material material)
    {
        _dbContext.Attach(material);
        _dbContext.Entry(material).Property("Name").IsModified = true;
        _dbContext.Entry(material).Property("Unit").IsModified = true;
        _dbContext.Entry(material).Property("Price").IsModified = true;
        _dbContext.Entry(material).Property("Quantity").IsModified = true;
        _dbContext.SaveChanges();
    }

    public double getTotalCostInTimeRange(DateTime startDate, DateTime endDate)
    {
        var services = _dbContext.Services.Where(s => s.BeginDate >= startDate && s.EndDate <= endDate);
        var materials = _dbContext.Materials.Where(m => services.Contains(m.Service));
        double totalCost = 0;
        foreach (var material in materials)
        {
            totalCost += (double)material.Price * material.Quantity;
        }
        return totalCost;
    }
}
