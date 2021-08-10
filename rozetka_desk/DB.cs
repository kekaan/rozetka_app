using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rozetka_desk
{
    class DB
    {   
        MySqlConnection connection = new MySqlConnection("Database=rozetka;Data Source=127.0.0.1;User Id=root;Password=root;port=3306");   

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public MySqlConnection getConnection()
        {

            return connection;
        }
    }
}
