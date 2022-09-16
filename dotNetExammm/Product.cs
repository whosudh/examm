using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace DotNetExamm.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Please Enter Product Name")]
        [StringLength(50,ErrorMessage ="The {0} value cannot exceed {1} characters.")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage ="Please Enter Rate")]
        public decimal Rate { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Product Description")]
        [StringLength(200, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Category Name")]
        [StringLength(50, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public static List<Product> GetAllProducts() {

            List<Product> products = new List<Product>();

            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = @"Data Source = (localdb)\MsSqlLocalDb;Initial Catalog = LabExam;Integrated Security = True";

            try
            {
                connection.Open();

                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = connection;
                cmdSelect.CommandType = CommandType.StoredProcedure;
                cmdSelect.CommandText = "FetchAll";

                SqlDataReader dataReader = cmdSelect.ExecuteReader();

                while (dataReader.Read()) {
                    Product product = new Product();
                    product.ProductId = (int)dataReader["ProductId"];
                    product.ProductName = (string)dataReader["ProductName"];
                    product.Rate = (decimal)dataReader["Rate"];
                    product.Description = (string)dataReader["Description"];
                    product.CategoryName = (string)dataReader["CategoryName"];

                    products.Add(product);
                }
                dataReader.Close();
            }
            catch (Exception e)
            {

            }
            finally {
                connection.Close();
            }
            
            return products;
        
        }

        public static void UpdateProduct(Product product) {

            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = @"Data Source = (localdb)\MsSqlLocalDb;Initial Catalog = LabExam;Integrated Security = True";

            try
            {
                connection.Open();

                SqlCommand cmdUpdate = new SqlCommand();
                cmdUpdate.Connection = connection;
                cmdUpdate.CommandType = CommandType.StoredProcedure;
                cmdUpdate.CommandText = "UpdateProduct";

                cmdUpdate.Parameters.AddWithValue("@ProductName",product.ProductName);
                cmdUpdate.Parameters.AddWithValue("@Rate",product.Rate);
                cmdUpdate.Parameters.AddWithValue("@Description", product.Description);
                cmdUpdate.Parameters.AddWithValue("@CategoryName", product.CategoryName);
                cmdUpdate.Parameters.AddWithValue("@ProductId",product.ProductId);

                cmdUpdate.ExecuteNonQuery();

               
            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }

        }

        public static Product GetProductById(int id)
        {

            SqlConnection connection = new SqlConnection();
            Product product = new Product();
            connection.ConnectionString = @"Data Source = (localdb)\MsSqlLocalDb;Initial Catalog = LabExam;Integrated Security = True";

            try
            {
                connection.Open();
                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = connection;
                cmdSelect.CommandType = CommandType.StoredProcedure;
                cmdSelect.CommandText = "GetById";
                cmdSelect.Parameters.AddWithValue("@ProductId", id);

                SqlDataReader dataReader = cmdSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    
                    product.ProductId = (int)dataReader["ProductId"];
                    product.ProductName = (string)dataReader["ProductName"];
                    product.Rate = (decimal)dataReader["Rate"];
                    product.Description = (string)dataReader["Description"];
                    product.CategoryName = (string)dataReader["CategoryName"];
                }
                dataReader.Close();

            }
            catch (Exception e)
            {

            }
            finally
            {
                connection.Close();
            }
            return product;
        }
    }
}