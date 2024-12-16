using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbModels;
using Npgsql;
using System.Text.Json;

namespace KursApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        [HttpGet]
        public List<Product> GetProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT get_products_json();", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) 
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }

            try
            {
                List<Product> products = JsonSerializer.Deserialize<List<Product>>(k);
                return products;
            }
            catch (Exception e)
            {
                return new List<Product>();
            }
        }

        [HttpGet]
        [Route("categories")]
        public List<Category> GetCategories()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT get_categories_json();", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) 
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }

            try
            {
                List<Category> categories = JsonSerializer.Deserialize<List<Category>>(k);
                return categories;
            }
            catch (Exception e)
            {
                return new List<Category>();
            }
        }

        [HttpGet]
        public List<Product> GetProductsByUser([FromUri] int u_id = 1)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT get_products_by_user_json({u_id});", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) 
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }

            try
            {
                List<Product> products = JsonSerializer.Deserialize<List<Product>>(k);
                return products;
            }
            catch (Exception e)
            {
                return new List<Product>();
            }
        }

        [HttpGet]
        public List<Product> GetProductsByStatus([FromUri] String status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT get_products_by_status_json('{status}');", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) 
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }

            try
            {
                List<Product> products = JsonSerializer.Deserialize<List<Product>>(k);
                return products;
            }
            catch (Exception e)
            {
                return new List<Product>();
            }
        }

        [HttpGet]
        public List<Product> GetProductsByCategory([FromUri] String category)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT get_products_by_category_json('{category}');", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }

            try
            {
                List<Product> products = JsonSerializer.Deserialize<List<Product>>(k);
                return products;
            }
            catch (Exception e)
            {
                return new List<Product>();
            }
        }


        [HttpGet]
        public Product GetProductByLot([FromUri] int lot_id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT get_product_by_lot_json('{lot_id}');", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }

            try
            {
                Product products = JsonSerializer.Deserialize<Product>(k);
                return products;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        public Product GetProduct(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT get_products_by_id_json({id});", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // 0 - индекс первого столбца
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }

            try
            {
                Product product = JsonSerializer.Deserialize<Product>(k);
                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public bool DeleteProduct(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT delete_product({id});", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // 0 - индекс первого столбца
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            try
            {
                BoolResult success = JsonSerializer.Deserialize<BoolResult>(k);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("confiscate/{id:int}")]
        public bool ConfiscateProduct(int id, [FromUri] int u_id = 1)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT confiscate_product({id}, {u_id});", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            try
            {
                BoolResult success = JsonSerializer.Deserialize<BoolResult>(k);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("offer/{id:int}")]
        public bool OfferProduct(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT offer_product({id});", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            try
            {
                BoolResult success = JsonSerializer.Deserialize<BoolResult>(k);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("send/{id:int}_aid={aid:int}_bet={bet:double}")]
        public bool SendToAuction(int id, int aid, double bet)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT send_product_to_auction({id}, {aid}, {bet.ToString().Replace(',', '.')});", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            try
            {
                BoolResult success = JsonSerializer.Deserialize<BoolResult>(k);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("add")]
        public bool AddProduct([FromUri] int u_id = 1, [FromUri] int cat_id = 1, [FromUri] String status = "normal",
            [FromUri] String name = null, [FromUri] String description = null)
        {
            if (name == null || description == null) return false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT add_product({u_id}, {cat_id}, '{status}', '{name}', '{description}');", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) // 0 - индекс первого столбца
                        {
                            k += reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            try
            {
                BoolResult success = JsonSerializer.Deserialize<BoolResult>(k);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}