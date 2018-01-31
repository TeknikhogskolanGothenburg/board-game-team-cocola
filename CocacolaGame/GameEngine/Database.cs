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
            Random s = new Random();
            
            SqlDbType[] Datatype = new SqlDbType[3] { SqlDbType.VarChar, SqlDbType.Int, SqlDbType.Int };
            string[] columns = new string[3] { "GameKey","GameStatus","GameRound" };
            string[] values = new string[3] { "","0",s.Next(1,52).ToString()};
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
                    player.Points = int.Parse(Reader["Points"].ToString());
                    player.PlayerStand = int.Parse(Reader["PlayerStand"].ToString());
                    Players.Add(player);

                }

                Reader.Close();
                SqlConnector.Close();
            }
        }
        public static void UpdatePlayerStand(string GameKey, string name, bool PlayerStand)
        {
            string value = "";
            if (PlayerStand)
            {
                value = "1";
            }
            else
            {
                value = "0";
            }
            try
            {

                if (Exists("GamePlayer", "GameKeyID", GameKey, SqlDbType.VarChar))
                {
                    SqlConnector.Open();

                    Command = new SqlCommand("UPDATE GamePlayer SET PlayerStand = @PlayerStand Where GameKeyID = @GameKeyID AND Player = @Player", SqlConnector);

                    Command.Parameters.Add("@GameKeyID", SqlDbType.VarChar).Value = GameKey;
                    Command.Parameters.Add("@Player", SqlDbType.VarChar).Value = name;
                    Command.Parameters.Add("@GameRound", SqlDbType.Int).Value = value;

                    Command.ExecuteNonQuery();

             


                }

            }
            catch
            {
     
            }
            finally
            {
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
                    player.Attemp = int.Parse(Reader["Attemp"].ToString());
                  


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



        public static int ReadGameRound(string GameKey )
        {
            int s = 0;
            try
            {
               
                if (Exists("GamePlayer", "GameKeyID", GameKey, SqlDbType.VarChar))
                {
                    SqlConnector.Open();

                    Command = new SqlCommand("Select * FROM Game Where GameKeyID = @GameKeyID", SqlConnector);

                    Command.Parameters.Add("@GameKeyID", SqlDbType.VarChar).Value = GameKey;

                    Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {


                        s = int.Parse(Reader["GameRound"].ToString());




                    }


                }

            }
            catch
            {
                
            }
            finally
            {
                Reader.Close();
                SqlConnector.Close();
                
            }
            return s;

        }

        public static bool UpdateGameRound(string GameKey, bool GameStart)
        {
            bool s = false;
            string GameValue = string.Empty;
            switch (GameStart)
            {
                case true:
                    GameValue = "0";
                    break;
                case false:
                    GameValue = "1";
                    break;
            }
            try
            {

                if (Exists("GamePlayer", "GameKeyID", GameKey, SqlDbType.VarChar))
                {
                    SqlConnector.Open();

                    Command = new SqlCommand("UPDATE Game SET GameRound = @GameRound Where GameKey = @GameKey", SqlConnector);

                    Command.Parameters.Add("@GameKey", SqlDbType.VarChar).Value = GameKey;
                    Command.Parameters.Add("@GameRound", SqlDbType.Int).Value = GameValue;

                    Command.ExecuteNonQuery();

                    s = true;


                }

            }
            catch
            {
                s = false;
            }
            finally
            {
                Reader.Close();
                SqlConnector.Close();

            }
            return s;

        }




        public static bool Update(string key , string NickName, string Points , string Attemps)
        {

            try { 
            string query = "UPDATE GamePlayer SET Points = @Points , Attemp = @Attemp WHERE PLayer = @player AND GameKeyID = @GameKeyID ";

                Command = new SqlCommand(query, SqlConnector);
            
            Command.Parameters.Add("@Points", SqlDbType.Int).Value = Points;
                  Command.Parameters.Add("@Attemp", SqlDbType.Int).Value = Attemps;
            Command.Parameters.Add("@player", SqlDbType.VarChar).Value = NickName;
                  Command.Parameters.Add("@GameKeyID", SqlDbType.VarChar).Value = key;
            
                SqlConnector.Open();
                Command.ExecuteNonQuery();
               
                return true;
            }
            catch
            {
              
                return false;
            }
            finally
            {
                SqlConnector.Close();
            }



        }














    }










}
