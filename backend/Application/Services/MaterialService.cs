
using Application.DTO.Material;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Application.Services;

public class MaterialService : IMaterialService
{
    private readonly IMapper _mapper;
    private readonly IMaterialRepository _materialRepository;

    public MaterialService(IMaterialRepository materialRepository, IMapper mapper)
    {
        _materialRepository = materialRepository;
        _mapper = mapper;
    }


    public int AddMaterial(NewMaterialDto material)
    {
        var materialEntity = _mapper.Map<Material>(material);
        var id = _materialRepository.AddMaterial(materialEntity);
        return id;
    }

    public void DeleteMaterial(int materialId)
    {
        _materialRepository.DeleteMaterial(materialId);
    }

    public object GetMaterial(int materialId)
    {
        var material = _materialRepository.GetMaterial(materialId);
        var materialDto = _mapper.Map<MaterialDto>(material);
        return materialDto;
    }

    public List<MaterialDto> GetMaterialsByServiceForList(int serviceId)
    {
        var materials = _materialRepository.GetMaterialsByService(serviceId)
            .ProjectTo<MaterialDto>(_mapper.ConfigurationProvider)
            .ToList();
        return materials;
    }

    public List<MaterialDto> GetMaterialsForList()
    {
        var materials = _materialRepository.GetAllMaterials()
            .ProjectTo<MaterialDto>(_mapper.ConfigurationProvider)
            .ToList();
        return materials;
    }

    public object UpdateMaterial(NewMaterialDto newMaterial)
    {
        var material = _mapper.Map<Material>(newMaterial);
        _materialRepository.UpdateMaterial(material);
        return material;
    }

    public double GetTotalCostInTime(DateTime startDate, DateTime endDate)
    {
        return _materialRepository.getTotalCostInTimeRange(startDate, endDate);
    }
}
