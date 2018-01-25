using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
   public class DatabaseProperties
    {

        private static readonly string TPlayer = "Player";
        public static string Player { get { return TPlayer; } }

        private static readonly string TGame = "Game";
        public static string Game { get { return TGame; } }

        private static readonly string TGamePlayer = "GamePlayer";
        public static string GamePlayer { get { return TGamePlayer; } }


        private static readonly string TNickname = "Nickname";
        public static string Nickname { get { return TNickname; } }


        private static readonly string TKey = "[Key]";
        public static string Key { get { return TKey; } }


        private static readonly string TKeyID = "KeyID";
        public static string KeyID { get { return TKeyID; } }


        private static readonly string TNicknameID = "NicknameID";
        public static string NicknameID { get { return TNicknameID; } }






    }
}
