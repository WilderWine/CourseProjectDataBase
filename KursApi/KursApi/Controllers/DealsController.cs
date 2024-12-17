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
        // api/deals?status=fjdbdj
        public string GetDealsByStatus([FromUri] String status)
        {
            try
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


                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        [HttpGet]
        // api/deals?uid=15
        public string GetDealsByUser([FromUri] int uid)
        {
            try
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


                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        [HttpGet]
        [Route("{did:int}")]
        // api/deals/8
        public string GetDeal(int did)
        {
            try
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

                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        [HttpPost]
        [Route("accept/{did:int}_number={cnumber}")]
        // api/deals/accept/11_number=dbudjdbnjhdbf
        public string AcceptDeal(int did, String number)
        {
            try
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

                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("close/{did:int}_number={cnumber}")]
        // api/deals/close/5_number=sjknjsbnksjdnbjdsk

        public string CloseDeal(int did, String number)
        {
            try
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("reject/{did:int}")]
        // api/deals/reject/7
        public string RejectDeal(int did)
        {
            try
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

                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("reoffer/{did:int}")]
        // api/deals/reoffer/4
        public string ReofferDeal(int did)
        {
            try
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

                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("expire/{did:int}")]
        // api/deals/expire/7
        public string ExpireDeal(int did)
        {
            try
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("upd/{did:int}_debt={debt:double}")]
        // api/deals/upd/8_debt=12.157
        public string SetDealDebt(int did, double debt)
        {
            try
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("upd/{did:int}_loan={loan:double}")]
        // api/deals/upd/8_loan=12.157
        public string SetDealLoan(int did, double loan)
        {
            try
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpGet]
        [Route("upd/{did:int}/{dtv}")]
        // api/deals/upd/8/2024-10-15_12-15-15
        public string SetDealEndterm(int did, string dtv)
        {
            try
            {
                var dt = dtv.Split('_');
                dtv = dt[0] +" "+ dt[1].Replace('-', ':');
                DateTime dt_dtv;
                if (!DateTime.TryParse(dtv, out dt_dtv))
                {
                    return "{\"success\" : false}";
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

                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("add")]
        // api/deals/add?pid=5&uid=5&enddtv=2024-10-15_13-55-56
        public string CreateDeal([FromUri] int pid, [FromUri] int uid, [FromUri] string enddtv, [FromUri] double debt, [FromUri] double loan)
        {
            try
            {
                var dt = enddtv.Split('_');
                string dtv = dt[0] +" "+ dt[1].Replace('-', ':');
                DateTime dt_dtv;
                if (!DateTime.TryParse(dtv, out dt_dtv))
                {
                    return "{\"success\" : false}";
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

                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        [HttpPost]
        [Route("check")]
        // api/deals/check
        public string CheckExpired()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string k = "";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new NpgsqlCommand($"SELECT check_expired();", connection);
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

    }
}