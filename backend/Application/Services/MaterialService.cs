using Application.DTO.Material;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public MaterialService(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }


        public int AddMaterial(NewMaterialDto material)
        {
            var materialEntity = _mapper.Map<Domain.Model.Material>(material);
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
            var material = _mapper.Map<Domain.Model.Material>(newMaterial);
            _materialRepository.UpdateMaterial(material);
            return material;
        }
    }
}
