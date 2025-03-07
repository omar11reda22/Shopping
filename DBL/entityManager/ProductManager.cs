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
    public class ProductManager
    {
        static DBManager dbManager = new DBManager();

        // Get all products
        public static ProductList GetAllProducts()
        {
            DataTable tb = dbManager.executedatatable("SELECT * FROM Products");
            return MapDataFromEntityToList(tb);
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
    }
}
