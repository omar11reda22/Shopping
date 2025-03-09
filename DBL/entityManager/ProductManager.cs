using DAL;
using DBL.Entity;
using DBL.EntityList;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL.entityManager
{
    public class ProductManager
    {
        static DBManager dbManager = new DBManager();

        // Get all products
        public static ProductList GetAllProducts()
        {
            DataTable tb = dbManager.executedatatable("SELECT * FROM Products");
            return MapDataFromEntityToList(tb);
        }

        // generate a method to mod stock on any product 

        public static bool changeproduct(int productid , int stock)
        {
            string query = "Update from Products set stock = @stock where ProductID = @productid";
            Dictionary<string, object> prms = new Dictionary<string, object>
            {
                {"@productid" ,productid },
                {"@stock" ,stock }
            };
             
           int result =  dbManager.ExecuteNonQuery(query, prms);
            return result > 0;  // result tru 
        }
        // generate a method to delete any product 

        public static bool deleteproduct(int productid)
        {
            string query = "delete from Products where ProductID = @productid";

            Dictionary<string, object> prms = new Dictionary<string, object> {
                {"@productid" ,productid}
            };

            int result =  dbManager.ExecuteNonQuery(query, prms);
            return result > 0; // delete done 

        }

        // generate a method to add a new product to admin 

        public static bool addnewproduct(Product product)
        {
            string query = "insert into Products (Name , Description , Price , Stock) values (@name , @description , @price , @stock)";
            Dictionary<string, object> prms = new Dictionary<string, object> {

                { "@name",product.Name },
                {"@description" ,product.Description },
                {"@price" ,product.Price },
                {"@stock" ,product.Stock }
            
            };
            int result =  dbManager.ExecuteNonQuery(query, prms);
            return result > 0;    
        }

        // Map DataTable to ProductList
        internal static ProductList MapDataFromEntityToList(DataTable tbb)
        {
            ProductList productList = new ProductList();
            foreach (DataRow dr in tbb.Rows)
            {
                productList.Add(FromDataRowToProduct(dr));
            }
            return productList;
        }

        // Convert DataRow to Product object
        internal static Product FromDataRowToProduct(DataRow dr)
        {
            Product product = new Product();

            if (int.TryParse(dr["ProductID"]?.ToString() ?? "-1", out int productId))
            {
                product.ProductID = productId;
            }

            product.Name = dr["Name"]?.ToString() ?? string.Empty;
            product.Description = dr["Description"]?.ToString() ?? string.Empty;

            if (decimal.TryParse(dr["Price"]?.ToString(), out decimal price))
            {
                product.Price = price;
            }

            if (int.TryParse(dr["Stock"]?.ToString(), out int stock))
            {
                product.Stock = stock;
            }

            if (DateTime.TryParse(dr["CreatedAt"]?.ToString(), out DateTime createdAt))
            {
                product.CreatedAt = createdAt;
            }

            return product;
        }


        public static bool ProductExists(string productName)
        {
            string query = "SELECT COUNT(*) FROM Products WHERE Name = @name";
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "@name", productName }
    };

            object result = dbManager.ExecuteNonQuery(query, parameters);

            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                return count > 0; // Returns true if product exists
            }

            return false;
        }

    }
}
