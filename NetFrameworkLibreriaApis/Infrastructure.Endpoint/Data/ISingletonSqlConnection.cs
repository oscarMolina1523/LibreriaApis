using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint
{
    public interface ISingletonSqlConnection
    {
        void CloseConnection();
        SqlDataAdapter CreateDataApdapter(string query);
        Task<DataTable> ExecuteQueryCommandAsync(string sql);
        SqlCommand GetCommand(string query);
        T GetDataRowValue<T>(DataRow row, string index, T defaultValue = default);
        void OpenConnection();
    }
}
