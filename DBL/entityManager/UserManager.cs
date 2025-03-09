using DAL;
using DBL.Entity;
using DBL.EntityList;
using Microsoft.Data.SqlClient;
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
            return mapdatafromentitytolist(tb);
        }

        // create a 2 method to change status [inactive] - usertype [admin]


        public static User getuserbyemail(string email)
        {
            string query = "select * from Users where Email = @email";
            Dictionary<string, object> prms = new Dictionary<string, object>
            {
                { "email", email}
            };
          DataTable tb =   dbManager.executedatatable(query,prms);

            return fromdatarowtouser(tb.Rows[0]);


        }

        public static bool Register(User newUser)
        {
            string query = "INSERT INTO Users (UserName, Email, PasswordHash, Phone, Address, Status, UserType) " +
                           "VALUES (@UserName, @Email, @PasswordHash, @Phone, @Address, @Status, @UserType)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@UserName", newUser.UserName },
                { "@Email", newUser.Email },
                { "@PasswordHash", newUser.PasswordHash }, // Make sure to hash the password before storing it!
                { "@Phone", newUser.Phone ?? (object)DBNull.Value },
                { "@Address", newUser.Address ?? (object)DBNull.Value },
                { "@Status", newUser.Status ?? "Active" },
                { "@UserType", newUser.UserType ?? "Customer" }
            };

            int result = dbManager.ExecuteNonQuery(query, parameters);
            return result > 0; // Returns true if a row was inserted
        }

        public static User login(string email , string password)
        {
            string query = "select * from Users where Email = @email AND passwordHash = @password AND Status = 'Active'";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                 { "email", email },
                { "password", password },
            };
        DataTable tb = dbManager.executedatatable(query, parameters);
            if(tb.Rows.Count == 1)
            {
                return fromdatarowtouser(tb.Rows[0]);
            }
            return null;

        }

        // generate a function to maping data table to entitylist 
        internal static UserList mapdatafromentitytolist(DataTable tbb)
        {
            UserList userlst = new UserList(); 
            foreach(DataRow dr in tbb.Rows)
            {
                userlst.Add(fromdatarowtouser(dr));
            }
            return userlst; 

        }
        internal static User fromdatarowtouser(DataRow dr)
        {
            User user = new User(); 
            if(int.TryParse(dr[0]?.ToString() ?? "-1", out int userid))
            {
                user.UserID = userid;
            }
            user.UserName = dr["UserName"]?.ToString() ?? string.Empty;
            user.Email = dr["Email"]?.ToString() ?? string.Empty;
            user.PasswordHash = dr["PasswordHash"]?.ToString() ?? string.Empty;
            user.Phone = dr["Phone"]?.ToString() ?? string.Empty;
            user.Address = dr["Address"]?.ToString() ?? string.Empty;
            user.Status = dr["Status"]?.ToString() ?? "Active";
            user.UserType = dr["UserType"]?.ToString() ?? "Customer";

            return user;

        }

        // Change UserType (Customer ↔ Admin)
        public static bool ChangeUserType(int userId)
        {
            string query = "UPDATE Users SET UserType = CASE WHEN UserType = 'Customer' THEN 'Admin' ELSE 'Customer' END WHERE UserID = @userId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@userId", userId }
    };

            int result = dbManager.ExecuteNonQuery(query, parameters);
            return result > 0; // Returns true if update is successful
        }

        // Change Status (Active ↔ Inactive)
        public static bool ToggleUserStatus(int userId)
        {
            string query = "UPDATE Users SET Status = CASE WHEN Status = 'Active' THEN 'Inactive' ELSE 'Active' END WHERE UserID = @userId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@userId", userId }
    };

            int result = dbManager.ExecuteNonQuery(query, parameters);
            return result > 0; // Returns true if update is successful
        }

    }
}
