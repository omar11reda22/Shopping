using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{

    /*
    in this layer dealing with direct with db steps or some instanse in this layer 
    1- connection with db pass conection string 
    2- write sqlcommand here commands 
    3- sqldataadapter here 
    4-  instanse from dta table 



     */
    public class DBManager
    {
        SqlConnection connection;
        SqlCommand cmd; 
        SqlDataAdapter adapter;
        DataTable dt;

        public DBManager()
        {
            connection = new SqlConnection("Data Source=.;Initial Catalog=ShopDB;Integrated Security=True;Trust Server Certificate=True");
        }

        // declare a method take a command and execute it as a table (datatable)
        // represent read operation 
        public DataTable executedatatable(string command)
        {
            cmd = new SqlCommand(command , connection);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable(); 
            adapter.Fill(dt); 
            return dt; 
        }
        
        // represent write operation [insert - update - delete] [parameters] 
        public DataTable executedatatable(string command,Dictionary<string , object> param)
        {
            cmd = new SqlCommand(command , connection); 
            foreach(var item in param)
            {
                cmd.Parameters.Add(new SqlParameter(item.Key , item.Value));
            }
            adapter = new SqlDataAdapter(cmd); 
            dt = new DataTable(); 
            adapter.Fill(dt);
            return dt;
        }

        public int ExecuteNonQuery(string command, Dictionary<string, object> Parms)
        {
            
            int r = -1;
            cmd = new SqlCommand(command, connection);
            cmd.Parameters.Clear();
            foreach (var item in Parms)
            {
                cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }
            if (connection.State != ConnectionState.Open)
                connection.Open();
            r = cmd.ExecuteNonQuery();
            connection.Close();
            return r;

        }


    }
}
