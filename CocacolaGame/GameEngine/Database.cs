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
        private SqlConnection User;
       public Database()
        {
            User = new SqlConnection(@"Data Source=DESKTOP-FAGSG73\SQLEXPRESS;Initial Catalog=GameDB;Integrated Security=True");
        }
        public void createGame()
        {
          
            string query = "INSERT INTO Game([Key]) Values('hgxahxah')";
            SqlCommand CreateGame = new SqlCommand(query, User);
            User.Open();
            CreateGame.ExecuteNonQuery();
           
        }
    }

    

}
