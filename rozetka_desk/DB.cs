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
        MySqlConnection connection = new MySqlConnection("Database=rozetka;Data Source=localhost;User Id=root;Password=root;");   

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
