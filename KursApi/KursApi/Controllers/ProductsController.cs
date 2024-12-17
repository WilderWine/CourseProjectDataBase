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
using System.Reflection.Metadata.Ecma335;

namespace KursApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        [HttpGet]
        // api/products
        public string GetProducts()
        {
            try
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


                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        [HttpGet]
        [Route("categories")]
        // api/products/categories
        public string GetCategories()
        {
             try
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
                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        [HttpGet]
        // api/products?u_id=5
        public string GetProductsByUser([FromUri] int u_id = 1)
        {
            try {
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

                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        [HttpGet]
        // api/products?status=hhh
        public string GetProductsByStatus([FromUri] String status)
        {
            try
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
                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        [HttpGet]
        // api/products?category=5
        public string GetProductsByCategory([FromUri] int category)
        {
            try { 

                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string k = "";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new NpgsqlCommand($"SELECT get_products_by_category_json({category});", connection);
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
                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }


        [HttpGet]
        // api/products?lot_id=1
        public string GetProductByLot([FromUri] int lot_id)
        {
            try{
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

                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        // api/products/5
        public string GetProduct(int id)
        {
            try { 
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

                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        // api/products/delete/5
        public string DeleteProduct(int id)
        {
            try { 
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("confiscate/{id:int}")]
        // api/products/confiscate/1?u_id=1
        public string ConfiscateProduct(int id, [FromUri] int u_id = 1)
        {
            try { 
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("offer/{id:int}")]
        // api/products/offer/5
        public string OfferProduct(int id)
        {
            try { 
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
                    return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}"; 
            }
        }

        [HttpPost]
        [Route("send/{id:int}_aid={aid:int}_bet={bet:double}")]
        // api/products/send/15_aid=1_bet=122.222
        public string SendToAuction(int id, int aid, double bet)
        {
            try { 
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("add")]
        // api/products/add?u_id=1&cat_id=1&name=prprpr&description=fignyakakayato
        public string AddProduct([FromUri] int u_id = 1, [FromUri] int cat_id = 1, [FromUri] String status = "normal",
            [FromUri] String name = null, [FromUri] String description = null)
        {
            try { 
                if (name == null || description == null) return "{\"success\" : false}";
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

    }
}