using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVCHelp.DAL.Contracts;

namespace WebMVCHelp.DAL
{
    public class Connection : Contracts.IConnection
    {
        private const string StrConn = "Server=.\\SQLExpress;Database=myDataBase;User Id=sa;Password=senha;MultipleActiveResultSets=true;";
        public Connection()
        {
            Open();
        }
        private SqlConnection _con = null;
        public void Close()
        {
            if (_con != null && _con.State == System.Data.ConnectionState.Open)
            {
                _con.Close();
            }
        }

        public SqlCommand CreateCommand()
        {
            if (_con == null || _con.State == System.Data.ConnectionState.Closed)
            {
                Open();
            }
            return _con.CreateCommand();
        }

        public void Dispose()
        {
            Close();
            _con.Dispose();
        }

        public SqlConnection Open()
        {
            if (_con == null)
            {
                _con = new SqlConnection(StrConn);
                _con.Open();
            }
            return _con;
        }

        SqlConnection IConnection.Connection()
        {
            return Open();
        }
    }
}
