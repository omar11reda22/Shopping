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

        // wanna to check if product already exist or not 
        // get all products in cart by user id 
        public static bool IsProductInCart(int userID, int productID)
        {
            string query = "SELECT COUNT(*) FROM Cart WHERE UserID = @UserID AND ProductID = @ProductID";

            Dictionary<string, object> prms = new Dictionary<string, object> {
                {"@UserID",userID },
                {"@ProductID" ,productID }

            }; 
         int result = DBManager.ExecuteNonQuery(query, prms);
            return result > 0; 
            
        }

        // if product already exist update stock 

        public static bool UpdateCartQuantity(int userID, int productID, int additionalQuantity)
        {
            string query = "UPDATE Cart SET Quantity = Quantity + @AdditionalQuantity WHERE UserID = @UserID AND ProductID = @ProductID";

            Dictionary<string, object> prms = new Dictionary<string, object> {
                { "@UserID",userID },
                {"@ProductID",productID },
                { "@AdditionalQuantity",additionalQuantity }
            
            };

            int resilt = DBManager.ExecuteNonQuery(query, prms);
            return resilt > 0; 
           
            
        }


        // create a method to delete from cart [delete product on cart by userid]

        public static bool deletefromcart(int productid , int userid)
        {
            string query = "delete from cart where ProductID = @productid and UserID = @userid";

            Dictionary<string, object> prms = new Dictionary<string, object> {
                {"@productid" ,productid},
                { "@userid",userid }
            };
            int result = DBManager.ExecuteNonQuery(query, prms);
            return result > 0; 
        }



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
            string query = "select c.CartID , p.Name , c.Quantity , c.AddedAt from Cart c join Products p on c.ProductID = p.ProductID where c.UserID = @userid";
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
                ProductName = row["Name"].ToString(),
                AddedAt = Convert.ToDateTime(row["AddedAt"])
                
            };
        }

    }
}
