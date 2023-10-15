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
    public class ProductoRepository : IProductoRepository
    {
        private readonly ISingletonSqlConnection _connectionBuilder;

        public ProductoRepository(ISingletonSqlConnection connectionBuilder)
        {
            _connectionBuilder = connectionBuilder;
        }

        public void Create(Producto producto)
        {
            string insertQuery = "INSERT INTO PRODUCTO (ID_PRODUCTO,DESCRIPCION_PRODUCTO,ID_CATEGORIA) VALUES(@ID, @Descripcion, @IdCategoria)";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(insertQuery);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = producto.Id
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = producto.DescripcionProducto
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdCategoria",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = producto.IdCategoria
                }
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public void Eliminar(Guid Id)
        {
            string deleteQuery = "DELETE FROM PRODUCTO WHERE ID_PRODUCTO = @ProductoId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(deleteQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@ProductoId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task<List<Producto>> Get()
        {
            string query = "SELECT * FROM PRODUCTO;";
            DataTable dataTable = await _connectionBuilder.ExecuteQueryCommandAsync(query);
            List<Producto> producto = dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();

            return producto;
        }

        public void ModificarProducto(Guid Id, ProductoDTO modificarProducto)
        {
            string updateQuery = "UPDATE PRODUCTO SET DESCRIPCION_PRODUCTO = @Descripcion, ID_CATEGORIA= @IdCategoria WHERE ID_PRODUCTO = @Id;";
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
                    ParameterName = "@Descripcion",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = modificarProducto.DescripcionProducto
                },
                new SqlParameter() {
                    Direction = ParameterDirection.Input,
                    ParameterName = "@IdCategoria",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = modificarProducto.IdCategoria
                },
                
            };
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
        }

        public Producto GetById(Guid Id)
        {
            Producto producto = null;
            string getQuery = "SELECT * FROM PRODUCTO WHERE ID_PRODUCTO = @ProductoId;";
            SqlCommand sqlCommand = _connectionBuilder.GetCommand(getQuery);
            SqlParameter parameter = new SqlParameter()
            {
                Direction = ParameterDirection.Input,
                ParameterName = "@ProductoId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = Id
            };
            sqlCommand.Parameters.Add(parameter);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                producto = new Producto
                {
                    Id = reader.GetGuid(reader.GetOrdinal("ID_PRODUCTO")),
                    DescripcionProducto = reader.GetString(reader.GetOrdinal("DESCRIPCION_PRODUCTO")),
                    IdCategoria = reader.GetGuid(reader.GetOrdinal("ID_CATEGORIA")),
                };
            }
            reader.Close();
            return producto;

        }

        private Producto MapEntityFromDataRow(DataRow row)
        {
            return new Producto()
            {
                Id = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_PRODUCTO"),
                DescripcionProducto = _connectionBuilder.GetDataRowValue<string>(row, "DESCRIPCION_PRODUCTO"),
                IdCategoria = _connectionBuilder.GetDataRowValue<Guid>(row, "ID_CATEGORIA"),
            };
        }
    }
}
