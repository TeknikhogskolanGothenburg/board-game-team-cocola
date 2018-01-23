using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System;

namespace GameEngine
{
    public class Database
    {
        public string generatedKey;
        private SqlConnection Connection;
        public Database()
        {
            // Wilmar DatabaseNyckel Data Source=LAPTOP-6J4IQ728\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True
            // lenas databasnyckel Data Source=LAPTOP-AMB9IU8B\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True
            Connection = new SqlConnection(@"Data Source=LAPTOP-6J4IQ728\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True;");
        }
        public string CreateKey()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            generatedKey = new String(stringChars);
            return generatedKey;
        }

        public void InsertKeyInDataBase()
        {
            bool checker = true;
            while (checker)
            {
                generatedKey = CreateKey();

                if (!CheckIfKeyExists(generatedKey))
                {
                    string query = "INSERT INTO Game([Key]) Values('" + generatedKey + "')";
                    SqlCommand CreateGame = new SqlCommand(query, Connection);
                    Connection.Open();
                    CreateGame.ExecuteNonQuery();
                    Connection.Close();
                    checker = false;


                }
            }
        }

        public bool CheckIfKeyExists(string key)
        {

            string query = "SELECT * FROM Game WHERE [Key] ='" + key + "'";
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