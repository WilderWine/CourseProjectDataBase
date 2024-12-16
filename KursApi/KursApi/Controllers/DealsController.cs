using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbModels;
using Microsoft.AspNetCore.Routing.Constraints;
using Npgsql;
using System.Text.Json;
using Microsoft.Ajax.Utilities;

namespace KursApi.Controllers
{
    [RoutePrefix("api/deals")]
    public class DealsController : ApiController
    {
        [HttpGet]
        public List<Deal> GetDealsByStatus([FromUri] String status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT get_deals_by_status_json('{status}');", connection);
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
                List<Deal> deals = JsonSerializer.Deserialize<List<Deal>>(k);
                return deals;
            }
            catch (Exception e)
            {
                return new List<Deal>();
            }
        }

        [HttpGet]
        public List<Deal> GetDealsByUser([FromUri] int uid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT get_deals_by_user_json({uid});", connection);
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
                List<Deal> deals = JsonSerializer.Deserialize<List<Deal>>(k);
                return deals;
            }
            catch (Exception e)
            {
                return new List<Deal>();
            }
        }

        [HttpGet]
        [Route("{did:int}")]
        public Deal GetDeal(int did)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT get_deal_by_id_json({did});", connection);
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
                Deal deal = JsonSerializer.Deserialize<Deal>(k);
                return deal;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("accept/{did:int}_number={cnumber}")]
        public bool AcceptDeal(int did, String number)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT accept_deal({did}, '{number}');", connection);
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
        [Route("close/{did:int}_number={cnumber}")]
        public bool CloseDeal(int did, String number)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT close_deal({did}, '{number}');", connection);
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
        [Route("reject/{did:int}")]
        public bool RejectDeal(int did)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT reject_deal({did});", connection);
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
        [Route("reoffer/{did:int}")]
        public bool ReofferDeal(int did)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT reoffefer_deal({did});", connection);
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
        [Route("expire/{did:int}")]
        public bool ExpireDeal(int did)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT set_deal_expired({did});", connection);
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
        [Route("upd/{did:int}_debt={debt:double}")]
        public bool SetDealDebt(int did, double debt)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT set_deal_debt({did}, {debt.ToString().Replace(',', '.')});", connection);
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
        [Route("upd/{did:int}_loan={loan:double}")]
        public bool SetDealLoan(int did, double loan)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT set_deal_loan({did}, {loan.ToString().Replace(',', '.')});", connection);
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

        [HttpGet]
        [Route("upd/{did:int}/{dtv}")]
        public bool SetDealEndterm(int did, string dtv)
        {
            var dt = dtv.Split('_');
            dtv = dt[0] +" "+ dt[1].Replace('-', ':');
            DateTime dt_dtv;
            if (!DateTime.TryParse(dtv, out dt_dtv))
            {
                return false;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT set_deal_endterm({did}, @dt_dtv);", connection);
                command.Parameters.AddWithValue("dt_dtv", dt_dtv);
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
        public bool CreateDeal([FromUri] int pid, [FromUri] int uid, [FromUri] string enddtv, [FromUri] double debt, [FromUri] double loan)
        {
            var dt = enddtv.Split('_');
            string dtv = dt[0] +" "+ dt[1].Replace('-', ':');
            DateTime dt_dtv;
            if (!DateTime.TryParse(dtv, out dt_dtv))
            {
                return false;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT create_deal({pid}, {uid}, @dt_dtv, {debt.ToString().Replace(',', '.')}," +
                    $"{loan.ToString().Replace(',', '.')});", connection);
                command.Parameters.AddWithValue("dt_dtv", dt_dtv);
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

    }
}