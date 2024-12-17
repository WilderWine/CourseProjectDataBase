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
        public string GetCardsAsync(int u_id) 
        {
            try { 
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

                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        [HttpPost]
        [Route("deduct")]

        // api/cards/deduct?number=dfbdjfbdjb&value=45.4
        public string DeductFromCard([FromUri] string number = null, [FromUri] double? value = null)
        {
            try { 
                if (value == null || number == null)
                {
                    return "{\"success\" : false}";
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("credit")]
        // api/cards/credit?number=dfbdjfbdjb&value=45.4
        public string CreditToCard([FromUri] string number = null, [FromUri] double? value = null)
        {
            try { 
                if (value == null || number == null)
                {
                    return "{\"success\" : false}";
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("add")]
        // api/cards/add?number=dfbdjfbdjb&u_id=1&remain=5645.58
        public string AddUser([FromUri] string number = null, [FromUri] int u_id = 1, [FromUri] double remain = 0)
        {
            try { 
                if (number == null) return  "{\"success\" : false}"; ;
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

    }
}