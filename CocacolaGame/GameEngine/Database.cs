using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GameEngine
{
   public static class DataBase
    {
        private static SqlCommand Command { get; set; }
        private static SqlDataReader Reader { get; set; }
        private static SqlConnection SqlConnector = new SqlConnection(@"Data Source=LAPTOP-6J4IQ728\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True");

       public static string CreateGameKey()
        {
            SqlDbType[] Datatype = new SqlDbType[2] { SqlDbType.VarChar, SqlDbType.Int};
            string[] columns = new string[2] { "GameKey","GameStatus" };
            string[] values = new string[2] { "","0" };
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

                if (InsertToDataBase("Game", columns, values, Datatype))
                {
                    return values[0];
                }

                ii++;
            }

            return null;
        }




        public static bool Exists(string TableName, string ColumnName, string DataSearch, SqlDbType dbType)
        {
            try
            {

                string query = "SELECT * FROM " + TableName + " WHERE " + ColumnName + " = " + "@" + ColumnName;
                Command = new SqlCommand(query, SqlConnector);
                Command.Parameters.Add("@" + ColumnName, dbType).Value = DataSearch;
                SqlConnector.Open();

                Reader = Command.ExecuteReader();
                if (Reader.HasRows)

                {
                    Reader.Close();
                    SqlConnector.Close();
                    return true;
                }
                else
                {
                    Reader.Close();
                    SqlConnector.Close();
                    return false;
                }
            }
            catch
            {
                Reader.Close();
                SqlConnector.Close();
                return false;
            }
        }

        public static void GetPlayers(List<Player> Players, string GameKey)
        {

            if (Exists("GamePlayer", "GameKeyID", GameKey, SqlDbType.VarChar))
            {
                SqlConnector.Open();

                Command = new SqlCommand("Select * FROM GamePlayer Where GameKeyID = @GameKeyID", SqlConnector);

                Command.Parameters.Add("@GameKeyID", SqlDbType.VarChar).Value = GameKey;
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    Player player = new Player();
                    player.Nickname = Reader["Player"].ToString();
                    player.IsAdmin = int.Parse(Reader["IsAdmin"].ToString());
                    player.Attemp = int.Parse(Reader["Attemp"].ToString());
                    player.Attemp = int.Parse(Reader["Points"].ToString());
                    Players.Add(player);

                }

                Reader.Close();
                SqlConnector.Close();
            }
        }
            public static void GetPlayers(Player player, string GameKey, string name)
            {

                if (Exists("GamePlayer", "GameKeyID", GameKey, SqlDbType.VarChar))
                {
                    SqlConnector.Open();

                    Command = new SqlCommand("Select * FROM GamePlayer Where GameKeyID = @GameKeyID AND Player = @Player", SqlConnector);

                    Command.Parameters.Add("@GameKeyID", SqlDbType.VarChar).Value = GameKey;
                Command.Parameters.Add("@Player", SqlDbType.VarChar).Value = name;
                Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                   
                        player.Nickname = Reader["Player"].ToString();
                    player.IsAdmin = int.Parse(Reader["IsAdmin"].ToString());
                      player.Points = int.Parse(Reader["Points"].ToString());
                    player.Points = int.Parse(Reader["Attemp"].ToString());
                  


                }

                    Reader.Close();
                    SqlConnector.Close();
                }

            }

        
        public static int GetGameStatus(string GameKey)
        {
            int Returner = 0; 
            if (Exists("GamePlayer", "GameKeyID", GameKey, SqlDbType.VarChar))
            {
                SqlConnector.Open();

                Command = new SqlCommand("Select GameStatus FROM Game Where GameKey = @GameKey", SqlConnector);

                Command.Parameters.Add("@GameKey", SqlDbType.VarChar).Value = GameKey;
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                  

                 Returner  = int.Parse(Reader["GameStatus"].ToString());
                    

                }
               
            }
            Reader.Close();
            SqlConnector.Close();
            return Returner;
        }

        public static void ChangeGameStatus(int Gamestatus, string GameKey)
        {
            try
            {
                string query = "UPDATE Game SET GameStatus = @GameStatus WHERE GameKey = @GameKey";
                Command = new SqlCommand(query, SqlConnector);
                Command.Parameters.Add("@GameStatus", SqlDbType.Int).Value = Gamestatus;
                Command.Parameters.Add("@GameKey", SqlDbType.VarChar).Value = GameKey;
                SqlConnector.Open();
                Command.ExecuteNonQuery();
            }
            catch
            {
                SqlConnector.Close();
            }
            finally
            {
                SqlConnector.Close();
            }
        }
        public static bool InsertToDataBase(string TableName, string[] ColumnNames, string[] Values, SqlDbType[] Datatype)
        {

            try
            {
                string fix = string.Empty;
                string fix2 = string.Empty;
                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    fix += ColumnNames[i] + ",";
                    fix2 += "@" + ColumnNames[i] + ",";

                }
                fix = fix.Remove(fix.Length - 1, 1);
                fix2 = fix2.Remove(fix2.Length - 1, 1);
                string query = "INSERT INTO " + TableName + " (" + fix + ") Values(" + fix2 + ")";

                Command = new SqlCommand(query, SqlConnector);

                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    string fixer = "@" + ColumnNames[i];
                    Command.Parameters.Add(fixer, Datatype[i]).Value = Values[i];

                }
                SqlConnector.Open();
                Command.ExecuteNonQuery();
                SqlConnector.Close();
                return true;
            }
            catch
            {
                SqlConnector.Close();
                return false;
            }



        }

















    }










}
