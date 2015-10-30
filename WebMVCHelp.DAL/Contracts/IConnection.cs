using System;
using System.Data.SqlClient;
namespace WebMVCHelp.DAL.Contracts
{
    public interface IConnection: IDisposable
    {
        SqlConnection Connection();
        SqlConnection Open();
        void Close();
        SqlCommand CreateCommand();
    }
}
