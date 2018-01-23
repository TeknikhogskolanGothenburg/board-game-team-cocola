using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;

namespace GameEngine
{
    public class Database
    {
        private SqlConnection Connection;
        public Database()
        {
            Connection = new SqlConnection(@"Data Source=DESKTOP-FAGSG73\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True");
        }
        public void CreateGame()
        {
            string query = "INSERT INTO Game([Key]) Values('hgxahxah')";
            SqlCommand CreateGame = new SqlCommand(query, Connection);
            Connection.Open();
            CreateGame.ExecuteNonQuery();
            Connection.Close();
        }
        public bool CheckGame(string key)
        {

            string query = "SELECT * FROM Game WHERE [Key] = 'key'";
            SqlCommand CreateGame = new SqlCommand(query, Connection);
            Connection.Open();
            SqlDataReader reader = CreateGame.ExecuteReader();
            if (reader.HasRows)

            {
                Connection.Close();
                return true;
            }
            else
            {
                Connection.Close();
                return false;
            }
        }
    }
}
