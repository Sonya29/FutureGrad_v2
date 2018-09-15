using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace SmartSch.Models
{
    public class Database
    {
        public MySqlConnection connection { get; set; }
        private string server;
        private string database;
        private string uid;
        private string password;
        private string connectionString;

        //Constructor
        public Database()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "VLE";
            uid = "K1406302";
            password = "firstClass";
            this.connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(this.connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.;
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }
   
        //Select statement
        public MySqlDataReader Select(String query)
        {
            MySqlDataReader rdr = null;
            try
            {
                connection = new MySqlConnection(this.connectionString);
                connection.Open();

                string stm = query;
                MySqlCommand cmd = new MySqlCommand(stm, connection);
                rdr = cmd.ExecuteReader();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            return rdr;
        }

        //Count statement
        public int Count(String query)
        {
            MySqlDataReader dataReader = null;
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                dataReader = cmd.ExecuteReader();
            }
            int num = 0;
            while (dataReader.Read())
            {
                num = num + 1;
            }

            return num;
        }
    }
}

