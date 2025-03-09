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
    public class orderManager
    {
      static DBManager dbmanager = new DBManager(); 

        // get all orders 

        public static orderList getallorders()
        {
            orderList orderList = new orderList();  
            string query = "select o.OrderID , u.UserName , p.Name ,  o.OrderDate , o.TotalAmount , o.Status from Orders o join Users u on o.UserID = u.UserID join OrderDetails d on d.OrderID = o.OrderID join Products p on d.ProductID = p.ProductID";

            DataTable tb =  dbmanager.executedatatable(query);

            
            foreach (DataRow dr in tb.Rows)
            {
                orderList.Add(MaporderFromDataRow(dr));
            }
            return orderList; 

        }

        // generate a method to get order by id [user profile] 


        public static orderList getordersbyuserid(int userid)
        {
            orderList orderList = new orderList(); 
            string query = " select o.OrderID , u.UserName , p.Name ,  o.OrderDate , o.TotalAmount , o.Status from Orders o join Users u on o.UserID = u.UserID join OrderDetails d on d.OrderID = o.OrderID join Products p on d.ProductID = p.ProductID where o.UserID = @userid";
            Dictionary<string, object> prms = new Dictionary<string, object> {
                { "@userid",userid }

            };

           DataTable tb =  dbmanager.executedatatable(query, prms);

            foreach(DataRow rr in tb.Rows)
            {
                orderList.Add(MaporderFromDataRow(rr));
            }
            return orderList; 
        }

        // generate a method to update any order status from pending to shipped
        // 

        public static bool changeorderstatus(int orderid)
        {
            string query = "update Orders set Status = 'Shipped' where OrderID = @orderid";
            Dictionary<string, object> prms = new Dictionary<string, object> {
                { "@orderid",orderid}
            };
           int result =  dbmanager.ExecuteNonQuery(query, prms);
            return result > 0; 
        }
        private static Order MaporderFromDataRow(DataRow row)
        {
            return new Order
            {
                OrderID = Convert.ToInt32(row["OrderID"]),
                TotalAmount = Convert.ToInt32(row["TotalAmount"]),
                ProductName = row["Name"].ToString(),
                Username = row["UserName"].ToString(),
                OrderDate = Convert.ToDateTime(row["OrderDate"]),
                Status = row["Status"].ToString() 
            };
        }
    }
}
