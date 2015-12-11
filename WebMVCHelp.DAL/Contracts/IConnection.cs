using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebMVCHelp.DAL.Contracts
{
    public interface IConnection: IDisposable
    {
        SqlConnection Connection();
        SqlConnection Open();
        Task<SqlConnection> OpenAsync();
        void Close();
        SqlCommand CreateCommand();
    }
}
