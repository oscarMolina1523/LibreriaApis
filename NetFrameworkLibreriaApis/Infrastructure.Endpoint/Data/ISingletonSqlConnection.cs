using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint
{
    public interface ISingletonSqlConnection
    {
        void CloseConnection();
        SqlDataAdapter CreateDataApdapter(string query);
        Task<DataTable> ExecuteQueryCommandAsync(SqlCommand command);
        Task<DataTable> ExecuteQueryCommandAsync(string sql);
        SqlCommand GetCommand(string query);
        Task<int> ExecuteNonQueryCommandAsync(SqlCommand command);
        T GetDataRowValue<T>(DataRow row, string index, T defaultValue = default);
        void OpenConnection();
    }
}
