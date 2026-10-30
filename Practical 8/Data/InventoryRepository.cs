using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Practical_8.Models;

namespace Practical_8.Data
{
    public class InventoryRepository
    {
        private readonly string connectionString =
            "Server=localhost;Port=3306;Database=inventorydb;Uid=root;Pwd=;";

        // GET ALL PRODUCTS
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT ProductId, ProductName, Category, Quantity, Price FROM products";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Category = reader["Category"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"])
                            });
                        }
                    }
                }
            }

            return products;
        }

        // GET PRODUCT BY ID
        public Product GetById(int id)
        {
            Product product = null;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT ProductId, ProductName, Category, Quantity, Price FROM products WHERE ProductId = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Category = reader["Category"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"])
                            };
                        }
                    }
                }
            }

            return product;
        }

        // INSERT PRODUCT
        public void Insert(Product product)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = @"INSERT INTO products
                                (ProductName, Category, Quantity, Price)
                                VALUES
                                (@ProductName, @Category, @Quantity, @Price)";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Category", product.Category);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // UPDATE PRODUCT
        public void Update(Product product)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE products
                                SET ProductName = @ProductName,
                                    Category = @Category,
                                    Quantity = @Quantity,
                                    Price = @Price
                                WHERE ProductId = @ProductId";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Category", product.Category);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // DELETE PRODUCT
        public void Delete(int id)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM products WHERE ProductId = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}