using System;

namespace Domain.Endpoint.Entities
{
    public interface IHaveCreationData
    {
        DateTime FechaCreacion { get; set; }
        Guid CreadoPor { get; set; }
    }

    public interface IHaveUpdateData
    {
        DateTime? UltimaActualizacion { get; set; }
        Guid? ActualizadoPor { get; set; }
    }

    public interface IAuditableEntity : IHaveCreationData, IHaveUpdateData { }

    public class AuditableEntity : BaseEntity, IAuditableEntity
    {
        public DateTime FechaCreacion { get; set; }
        public Guid CreadoPor { get; set; } // Owner Unique Id
        public DateTime? UltimaActualizacion { get; set; }
        public Guid? ActualizadoPor { get; set; }
    }
}
