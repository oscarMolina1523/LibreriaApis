using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.Endpoint.Dtos;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class DetalleProductoRepository : IDetalleProductoRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;

        public DetalleProductoRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(DetalleProducto detalleProducto)
        {
            string insertQuery = "INSERT INTO DETALLE_PRODUCTO (ID_DETALLE_PRODUCTO,ID_PRODUCTO, ID_MARCA, ID_UNIDAD_MEDIDA, ID_MATERIAL) VALUES(@ID, @IdProducto, @IdMarca, @IdUnidadMedida, @IdMaterial)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = detalleProducto.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdProducto",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = detalleProducto.IdProducto
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdMarca",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = detalleProducto.IdMarca
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdUnidadMedida",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = detalleProducto.IdUnidadMedida
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdMaterial",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = detalleProducto.IdMaterial
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM DETALLE_PRODUCTO WHERE ID_DETALLE_PRODUCTO = @DetalleProductoId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@DetalleProductoId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<DetalleProducto>> Get()
        {
            string query = "SELECT * FROM DETALLE_PRODUCTO;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<DetalleProducto> detalle = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return detalle;
        }

        public void ModificarDetalle(Guid Id, DetalleProductoDTO modificarDetalle)
        {
            string updateQuery = "UPDATE DETALLE_PRODUCTO SET ID_PRODUCTO = @IdProducto, ID_MARCA = @IdMarca, ID_UNIDAD_MEDIDA=@IdUnidadMedida, ID_MATERIAL=@IdMaterial WHERE ID_DETALLE_PRODUCTO = @Id;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(updateQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdProducto",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = modificarDetalle.IdProducto
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdMarca",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = modificarDetalle.IdMarca
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdUnidadMedida",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = modificarDetalle.IdUnidadMedida
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdMaterial",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = modificarDetalle.IdMaterial
                },
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public DetalleProducto GetById(Guid Id)
        {
            DetalleProducto detalle = null;
            string getQuery = "SELECT * FROM DETALLE_PRODUCTO WHERE ID_DETALLE_PRODUCTO = @DetalleId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@DetalleId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                detalle = new DetalleProducto
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_DETALLE_PRODUCTO")),
                    IdProducto = reader.GetGuid(reader.GetOrdinal("ID_PRODUCTO")),
                    IdMarca = reader.GetGuid(reader.GetOrdinal("ID_MARCA")),
                    IdUnidadMedida = reader.GetGuid(reader.GetOrdinal("ID_UNIDAD_MEDIDA")),
                    IdMaterial = reader.GetGuid(reader.GetOrdinal("ID_MATERIAL")),
                };
            }
            reader.Close();
            return detalle;


        }

        private DetalleProducto MapEntityFromDataRow(DataRow row)
        {
            return new DetalleProducto()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_DETALLE_PRODUCTO"),
                IdProducto = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_PRODUCTO"),
                IdMarca = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_MARCA"),
                IdMaterial = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_MATERIAL"),
                IdUnidadMedida = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_UNIDAD_MEDIDA"),
            };
        }

    }
}
