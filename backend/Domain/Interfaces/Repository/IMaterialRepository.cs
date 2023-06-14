using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IMaterialRepository
    {
        void DeleteMaterial(int materialId);
        int AddMaterial(Material material);
        IQueryable<Material> GetAllMaterials();
        IQueryable<Material> GetMaterialsByService(int serviceId);
        Material GetMaterial(int materialId);
        void UpdateMaterial(Material material);
    }
}
