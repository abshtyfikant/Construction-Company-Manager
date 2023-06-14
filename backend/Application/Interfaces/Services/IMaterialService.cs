using Application.DTO.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IMaterialService
    {
        int AddMaterial(NewMaterialDto material);
        List<MaterialDto> GetMaterialsForList();
        List<MaterialDto> GetMaterialsByServiceForList(int serviceId);
        object GetMaterial(int materialId);
        object UpdateMaterial(NewMaterialDto newMaterial);
        void DeleteMaterial(int materialId);
    }
}
