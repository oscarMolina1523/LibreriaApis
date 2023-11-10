using Domain.Endpoint.Dtos;
using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IMaterialService
    {
        Task<List<Material>> GetAll();

        Task<Material> GetById(Guid Id);

        Material CrearMaterial(MaterialDTO nuevaMaterial);

        Task<Material> EliminarMaterial(Guid Id);
    }
}
