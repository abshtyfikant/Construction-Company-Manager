using Application.DTO.Material;

namespace Application.Interfaces.Services;

public interface IMaterialService
{
    int AddMaterial(NewMaterialDto material);
    List<MaterialDto> GetMaterialsForList();
    List<MaterialDto> GetMaterialsByServiceForList(int serviceId);
    object GetMaterial(int materialId);
    object UpdateMaterial(NewMaterialDto newMaterial);
    void DeleteMaterial(int materialId);
    double GetTotalCostInTime(DateTime startDate, DateTime endDate);
}
