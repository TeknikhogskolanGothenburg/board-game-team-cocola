using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System;

namespace GameEngine
{
    // Wilmar DatabaseNyckel Data Source=LAPTOP-6J4IQ728\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True
    // lenas databasnyckel Data Source=LAPTOP-AMB9IU8B\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True

    public class Database
    {
        public  static string generatedKey;
        private static SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-AMB9IU8B\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True;");
        
        private static string CreateKey()
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

        public static void InsertKeyInDataBase()
        {
            bool checker = true;
            int i = 0;
            while (checker && i < 10)
            {
                
             
                if (Insert("Game","[Key]", CreateKey()))
                {

                    
                    checker = false;


                }
                i++;
            }
        }

        public static bool Exists(string TableName , string TableObject, string TableSearch  )
        {

            string query = "SELECT * FROM "+TableName+" WHERE "+TableObject+" = '" +TableSearch + "'";
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

        public static bool Insert(string TableName, string TableObject, string TableInsearch)
        {
            if (!Exists(TableName, TableObject, TableInsearch))
            {
                try
                {
                    string query = "INSERT INTO " + TableName + "(" + TableObject + ") Values('" + TableInsearch + "')";
                    SqlCommand CreateGame = new SqlCommand(query, Connection);
                    Connection.Open();
                    CreateGame.ExecuteNonQuery();
                    Connection.Close();
                    generatedKey = TableInsearch;
                    return true;
                }
                catch
                {
                    Connection.Close();
                    generatedKey = "test";
                    return false;
                }

            }
            else
            {
                Connection.Close();
                generatedKey = "test1";
                return false;
            }
        }

        public static bool InsertToJoin(string JoinTableName, string JoinTableObject1, string JoinTableObject2, string TableInsearch1, string TableInsearch2)
        {
            
                try
                {
                    string query = "INSERT INTO " + JoinTableName + "(" + JoinTableObject1 + ", " + JoinTableObject2 + ") Values('" + TableInsearch1 + "', '" + TableInsearch2 + "')";
                    SqlCommand CreateGame = new SqlCommand(query, Connection);
                    Connection.Open();
                    CreateGame.ExecuteNonQuery();
                    Connection.Close();
                    return true;
                }
                catch
                {
                    Connection.Close();
                    return false;
                }

            
           
        }

    }
}