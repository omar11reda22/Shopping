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
    public class cartmanager
    {
        static DBManager DBManager = new DBManager();

        public static bool AddToCart(int userID, int productID, int quantity)
        {
        
                string query = "INSERT INTO Cart (UserID, ProductID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";

                Dictionary<string, object> prms = new Dictionary<string, object>
            {
                { "@UserID",userID },
                { "@ProductID", productID},
                { "@Quantity",quantity }
            };

                int result = DBManager.ExecuteNonQuery(query, prms);
                return result > 0;
            
           
        }

        // wanna to create a method get cart items by userid 

        public static CartList getcartbyuserid(int userid)
        {
            CartList cts = new CartList();
            string query = "SELECT Cart.CartID, Cart.Quantity, Products.Name FROM Cart                            JOIN Products ON Products.ProductID = Cart.ProductID                                          WHERE Cart.UserID = @UserID";
            Dictionary<string, object> prm = new Dictionary<string, object> 
            {
                { "@UserID",userid }
            };

            DataTable tb =  DBManager.executedatatable(query, prm);

            foreach (DataRow row in tb.Rows)
            {
                cts.Add(MapCartFromDataRow(row));
            }

            return cts;
        }

        private static Cart MapCartFromDataRow(DataRow row)
        {
            return new Cart
            {
                CartID = Convert.ToInt32(row["CartID"]),
                Quantity = Convert.ToInt32(row["Quantity"]),
                ProductName = row["Name"].ToString()
            };
        }

    }
}
