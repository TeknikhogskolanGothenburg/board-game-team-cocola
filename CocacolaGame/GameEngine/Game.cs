using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GameEngine
{
    public class Game
    {
        public List<Player> Players = new List<Player>();
        public enum _IsAdmin { Yes, NO }
        public Player _CurrentPlayer = new Player();
        private int _GameStatus { get; set; }
        public int GameStatus { get { return _GameStatus; } }
        private bool _IsGameCreated { get; set; }
        public bool IsGameCreated { get { return _IsGameCreated; } }
        private string _GetGameKey { get; set; }
        public string GetGameKey { get { return _GetGameKey; } }

        public Game(string Gamekey)
        {
            _IsGameCreated = DataBase.Exists("Game", "GameKey", Gamekey, System.Data.SqlDbType.VarChar);
            if (_IsGameCreated)
            {
                DataBase.GetPlayers(Players, Gamekey);
                ReadGameStatus(Gamekey);
                _GetGameKey = Gamekey;

            }
        }
        public void GetCurrentPlayer(string name)
        {
            DataBase.GetPlayers(_CurrentPlayer, _GetGameKey,name);

        }
        public bool InsertPlayer(string Player, _IsAdmin IsAdmin)
        {
            bool Returner = false;
            SqlDbType[] SqlDatatypes = new SqlDbType[] {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int,SqlDbType.Int, SqlDbType.Int };
            string[] Values = new string[] {GetGameKey,Player,"","0","5" };
            string[] Colums = new string[] {"GameKeyID","Player", "IsAdmin", "Points", "Attemp" };
            switch (IsAdmin)
            {
                case _IsAdmin.Yes:
                    Values[2] = "1";
                    break;
                case _IsAdmin.NO:
                    Values[2] = "0";
                    break;

            }
             if(DataBase.InsertToDataBase("GamePlayer", Colums, Values, SqlDatatypes))
                {
                     DataBase.GetPlayers(_CurrentPlayer, _GetGameKey,Player);
                     Returner = true;
                }
            else
            {
                Returner = false;
            }
            return Returner;
           
        }
        public void ChangeGameStatus(bool GameStart)
        {
            switch (GameStart)
            {
                case true:
                    DataBase.ChangeGameStatus(1, GetGameKey);
                    break;
                case false:
                    DataBase.ChangeGameStatus(0, GetGameKey);
                    break;
            }
          
            
         }
        public void ReadPlayers(string Gamekey)
        {
            DataBase.GetPlayers(Players, Gamekey);
        }
        public void ReadGameStatus(string Gamekey)
        {
            _GameStatus = DataBase.GetGameStatus(Gamekey);
        }
    }
}
