﻿using System;

namespace Domain.Endpoint.Dtos
{
    public class DetalleProductoDTO
    {
        public Guid IdUnidadMedida { get; set; }

        public Guid IdMarca { get; set; }

        public Guid IdMaterial { get; set; }

        public Guid IdProducto { get; set; }
    }
}
