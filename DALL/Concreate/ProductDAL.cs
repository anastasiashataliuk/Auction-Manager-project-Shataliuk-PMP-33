using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Concrete
{
    public class ProductDal : IProductDal
    {
        private readonly SqlConnection _connection;

        public ProductDal(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<Product> GetAll()
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT product_id, name, description, category FROM TblProduct";

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Product> products = new List<Product>();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Product_Id = Convert.ToInt32(reader["product_id"]),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Category = reader["category"].ToString() // Змінено на просто Category
                    });
                }

                _connection.Close();
                return products;
            }
        }

        public Product GetById(int id)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT product_id, name, description, category FROM TblProduct WHERE product_id = @product_id";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("product_id", id);

                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Product product = null;
                if (reader.Read())
                {
                    product = new Product
                    {
                        Product_Id = Convert.ToInt32(reader["product_id"]),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Category = reader["category"].ToString() // Змінено на просто Category
                    };
                }
                _connection.Close();

                if (product == null)
                    throw new NullReferenceException("Invalid Product ID");

                return product;
            }
        }

        public Product Insert(Product product)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO TblProduct (name, description, category) OUTPUT INSERTED.product_id VALUES (@name, @description, @category)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("name", product.Name);
                command.Parameters.AddWithValue("description", product.Description);
                command.Parameters.AddWithValue("category", product.Category); // Змінено на просто Category

                _connection.Open();
                product.Product_Id = Convert.ToInt32(command.ExecuteScalar());
                _connection.Close();
                return product;
            }
        }

        // Додайте методи для оновлення та видалення, якщо це необхідно
        public void Delete(int id)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM TblProduct WHERE product_id = @product_id";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@product_id", id);

                _connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                _connection.Close();

                if (rowsAffected == 0)
                {
                    throw new Exception("Delete failed. Product not found.");
                }
            }
        }
        public void Update(Product product)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE TblProduct SET name = @name, description = @description, category = @category WHERE product_id = @product_id";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@product_id", product.Product_Id);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@category", product.Category); // Виправлено на CategoryId

                _connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                _connection.Close();

                if (rowsAffected == 0)
                {
                    throw new Exception("Update failed. Product not found.");
                }
            }
        }
    }
}
