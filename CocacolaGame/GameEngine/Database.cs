using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System;
using System.Data;
using System.Collections.Generic;

namespace GameEngine
{
    // Wilmar DatabaseNyckel Data Source=LAPTOP-6J4IQ728\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True
    // lenas databasnyckel Data Source=LAPTOP-AMB9IU8B\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True

    public class Database
    {
        public  static string generatedKey;
        private static SqlCommand Command { get; set; }
        private static SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-6J4IQ728\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True;");
        
        public static string CreateGameKey()
        {
            SqlDbType[] Datatype = new SqlDbType[1]{ SqlDbType.VarChar };
            string[] columns = new string[1] {"GameKey"};
            string[] values = new string[1] {""};
            bool Check = false;
            int ii = 0;
            while (!Check && ii < 100)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var stringChars = new char[5];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

            values[0] = new String(stringChars);

                if (InsertToDataBase("Game", columns, values,Datatype))
                {
                    return values[0];
                }

                ii++;
            }

            return null;
        }

        //public static void InsertKeyInDataBase()
        //{
        //    bool checker = true;
        //    int i = 0;
        //    while (checker && i < 10)
        //    {
                
             
        //        if (Insert("Game","[Key]", CreateKey()))
        //        {

        //         generatedKey =
        //           checker = false;


        //        }
        //        i++;
        //    }
        //}

        public static bool Exists(string TableName , string ColumnName, string DataSearch , SqlDbType dbType )
        {
            try
            {

                string query = "SELECT * FROM " + TableName + " WHERE " + ColumnName + " = " + "@" + ColumnName;
                Command = new SqlCommand(query, Connection);
                Command.Parameters.Add("@" + ColumnName, dbType).Value = DataSearch;
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
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
            catch
            {
                Connection.Close();
                return false;
            }
        }
        public static void GetData(List<Player> Players, string GameKey)
        {
           
          
            Connection.Open();
          
              Command = new SqlCommand("Select NickNameID FROM GamePlayer Where KeyID = @KeyID",Connection);

            Command.Parameters.Add("@KeyID", SqlDbType.VarChar).Value = GameKey;
            SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                Player player = new Player();
                player.Name  = reader["NickNameID"].ToString();
                Players.Add(player);
                   
                }

           
        }
           
        

        public static bool InsertToDataBase(string TableName, string[] ColumnNames, string[]Values, SqlDbType[] Datatype)
        {

            try
            {
                string fix = string.Empty;
                string fix2 = string.Empty;
                for (int i = 0; i < ColumnNames.Length; i++)
                { 
                 fix += ColumnNames[i] +",";
                    fix2 += "@"+ColumnNames[i] + ",";
                 
                }
                 fix =  fix.Remove(fix.Length - 1, 1);
                fix2 = fix2.Remove(fix2.Length - 1, 1);
                string query = "INSERT INTO " + TableName + " (" + fix + ") Values("+ fix2 +")";

                Command = new SqlCommand(query, Connection);

                for(int i = 0; i < ColumnNames.Length; i++)
                {
                    string fixer = "@" + ColumnNames[i];
                    Command.Parameters.Add(fixer,Datatype[i] ).Value = Values[i];
              
                }
                Connection.Open();
                Command.ExecuteNonQuery();
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