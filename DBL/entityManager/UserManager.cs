using DAL;
using DBL.Entity;
using DBL.EntityList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL.entityManager
{
    public static class UserManager
    {
        // access DAL to cdealing with DB 
        static DBManager dbManager = new DBManager();

        // get all users 
        public static UserList selectallusers()
        {
            DataTable tb = dbManager.executedatatable("select * from user");
            return tb;
        }
        // generate a function to maping data table to entitylist 
        internal static UserList mapdatafromentitytolist(DataTable tbb)
        {
            UserList userlst = new UserList(); 
            foreach(DataRow dr in tbb.Rows)
            {

            }

        }
        internal static User fromdatarowtouser(DataRow dr)
        {
            User user = new User(); 
            if(int.TryParse(dr[0]?.ToString() ?? "-1", out int userid))
            {
                user.UserID = userid;
            }
            
        }


    }
}
