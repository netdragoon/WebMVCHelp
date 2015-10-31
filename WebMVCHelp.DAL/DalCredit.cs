using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    }
}
