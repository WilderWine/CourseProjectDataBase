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
    [RoutePrefix("api/cards")]
    public class CardsController : ApiController
    {
        // GET api/cards/1
        [HttpGet]
       // [Route("{id:int}")]
        public List<Card> GetCards(int u_id) 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();


                var command = new NpgsqlCommand($"select get_cards_by_user({u_id});", connection);
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
                List<Card> userLogs = JsonSerializer.Deserialize<List<Card>>(k);
                return userLogs;
            }
            catch (Exception e)
            {
                return new List<Card>();
            }
        }

        [HttpPost]
        [Route("deduct")]

        public bool DeductFromCard([FromUri] string number = null, [FromUri] double? value = null)
        {
            if (value == null || number == null)
            {
                return false;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT deduct_from_card('{number}', {value.ToString().Replace(',', '.')});", connection);
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
        [Route("credit")]

        public bool CreditToCard([FromUri] string number = null, [FromUri] double? value = null)
        {
            if (value == null || number == null)
            {
                return false;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT credit_to_card('{number}', {value.ToString().Replace(',', '.')});", connection);
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
        [Route("add")]
        public bool AddUser([FromUri] string number = null, [FromUri] int u_id = 1, [FromUri] double remain = 0)
        {
            if (number == null) return false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT create_card('{number}',{remain.ToString().Replace(',', '.')}, {u_id});", connection);
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