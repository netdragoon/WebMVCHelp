using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PagedList;
using WebMVCHelp.DAL.Contracts;
using WebMVCHelp.Models;

namespace WebMVCHelp.DAL
{
    public class DalCredit : Interfaces.IDalCredit
    {
        public IConnection Connection { get; private set; }
        public DalCredit(IConnection Connection)
        {
            this.Connection = Connection;
        }
        public Credit Add(Credit Model)
        {
            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Credit(Description) VALUES(@Description);SELECT @@IDENTITY;";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = Model.Description;
                Model.CreditId = int.Parse(command.ExecuteScalar().ToString());
            }
            return Model;
        }

        public IEnumerable<Credit> All()
        {
            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "SELECT CreditId, Description FROM Credit ORDER BY Description";
                command.CommandType = CommandType.Text;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new Credit(reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
        }

        public bool Edit(Credit Model)
        {
            bool _return = false;

            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "UPDATE Credit SET Description=@Description WHERE CreditId=@CreditId";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = Model.Description;
                command.Parameters.Add("@CreditId", SqlDbType.Int).Value = Model.CreditId;
                _return = (command.ExecuteNonQuery() > 0);
            }

            return _return;
            
        }

        public bool Remove(params object[] Keys)
        {
            bool _return = false;

            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Credit WHERE CreditId=@CreditId";
                command.CommandType = CommandType.Text;                
                command.Parameters.Add("@CreditId", SqlDbType.Int).Value = Keys[0];
                _return = (command.ExecuteNonQuery() > 0);
            }
            
            return _return;

        }

        public bool Remove(Credit Model)
        {
            return Remove(Model.CreditId);
        }

        public Credit Find(params object[] Keys)
        {
            Credit credit = null;

            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "SELECT CreditId, Description FROM Credit WHERE CreditId=@CreditId";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@CreditId", SqlDbType.Int).Value = Keys[0];
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    credit = new Credit(reader.GetInt32(0), reader.GetString(1));
                }
            }

            return credit;
        }
        public async Task<Credit> FindAsync(params object[] Keys)
        {
            Credit credit = null;

            using (SqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = "SELECT CreditId, Description FROM Credit WHERE CreditId=@CreditId";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@CreditId", SqlDbType.Int).Value = Keys[0];
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    await reader.ReadAsync();
                    credit = new Credit(reader.GetInt32(0), reader.GetString(1));
                }
            }

            return credit;
        }
        public IPagedList<Credit> All(int Page, int Total = 10)
        {
            return All(null, Page, Total);
        }

        public IPagedList<Credit> All(string filtro, int Page, int Total = 10)
        {            
            IList<Credit> Credits = new List<Credit>();
            int TotaItems = 0;
            using (SqlCommand command = Connection.CreateCommand())
            {
                string SQL = "SELECT* FROM( ";
                SQL += "SELECT ROW_NUMBER() OVER(ORDER BY Description) AS NUMBER,CreditId, Description FROM Credit ";
                if (!string.IsNullOrEmpty(filtro))
                {
                    SQL += "WHERE Description LIKE @Filtro ";
                }
                SQL += ") as TBL ";
                SQL += "WHERE NUMBER BETWEEN((@Page - 1) * @Total + 1) AND (@Page * @Total) ";                
                SQL += "ORDER BY Description ";
                command.CommandText = SQL;
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@Page", SqlDbType.Int).Value = Page;
                command.Parameters.Add("@Total", SqlDbType.Int).Value = Total;
                if (!string.IsNullOrEmpty(filtro))
                {
                    command.Parameters.Add("@Filtro", SqlDbType.VarChar).Value = string.Format("%{0}%", filtro);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Credits.Add(new Credit(reader.GetInt32(1), reader.GetString(2)));
                    }
                }
                if (!string.IsNullOrEmpty(filtro))
                {
                    command.Parameters.RemoveAt("@Page");
                    command.Parameters.RemoveAt("@Total");
                }
                else
                {
                    command.Parameters.Clear();
                }
                command.CommandText = string.IsNullOrEmpty(filtro) ?
                    "SELECT Count(*) Rows FROM Credit" :
                    "SELECT Count(*) Rows FROM Credit WHERE Description LIKE @Filtro";
                TotaItems = int.Parse(command.ExecuteScalar().ToString());
            }
            return new StaticPagedList<Credit>(Credits, Page, Total, TotaItems);
        }

        
    }
}
//DECLARE @PageNumber AS INT, @RowspPage AS INT
//SET @PageNumber = 2
//SET @RowspPage = 5
//SELECT* FROM(
//             SELECT ROW_NUMBER() OVER(ORDER BY ID_EXAMPLE) AS NUMBER,
//                    ID_EXAMPLE, NM_EXAMPLE, DT_CREATE FROM TB_EXAMPLE
//               ) AS TBL
//WHERE NUMBER BETWEEN((@PageNumber - 1) * @RowspPage + 1) AND(@PageNumber* @RowspPage)
//ORDER BY ID_EXAMPLE
