using Domain.Endpoint.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IMaterialService
    {
        List<MaterialDTO> GetAll();

        MaterialDTO GetById(Guid Id);

        MaterialDTO CrearMaterial(MaterialDTO nuevaMaterial);

        void EliminarMaterial(Guid Id);
    }
}
