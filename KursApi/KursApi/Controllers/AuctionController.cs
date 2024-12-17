using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DbModels;
using Microsoft.AspNetCore.Mvc.Internal;
using Npgsql;
using System.Text.Json;
using Antlr.Runtime.Misc;

namespace KursApi.Controllers
{
    [RoutePrefix("api/auction")]
    public class AuctionController : ApiController
    {

        [HttpGet]
        // api/auction

        public string GetAuctions()
        {
            try{
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string k = "";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new NpgsqlCommand($"SELECT get_auctions_json();", connection);
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
        [Route("lots/{id:int}")]

        // api/auction/lots/2
        public string GetLotById(int id)
        {
            try { 
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string k = "";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new NpgsqlCommand($"SELECT get_lot_by_id_json({id});", connection);
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
        [Route("lots/aid={aid:int}")]
        // api/auction/lots/aid=12

        public string GetLotsByAuction(int aid) 
        {
            try { 
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string k = "";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new NpgsqlCommand($"SELECT get_lots_by_auction_json({aid});", connection);
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
        [Route("lots/uid={uid:int}")]
        // api/auction/lots/uid=2

        public string GetLotsByUser(int uid)
        {
            try { 
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string k = "";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new NpgsqlCommand($"SELECT get_lots_by_user_json({uid});", connection);
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
        [Route("add/sdtv={startdtv}/edtv={enddtv}")]
        // api/auction/add/sdtv=2024-11-11_12-12-55/edtv=2024-11-11_12-12-55

        public string CreateAuction(String startdtv, String enddtv)
        {
            try{
                var sdt = startdtv.Split('_');
                string dtv = sdt[0] + " " + sdt[1].Replace('-', ':');
           
                var edt = enddtv.Split('_');
                string etv = edt[0] + " " + edt[1].Replace('-', ':');
            
                DateTime dt_sdt;
                if (!DateTime.TryParse(dtv, out dt_sdt))
                {
                     return "{\"success\" : false}";
                }
                DateTime dt_edt;
                if (!DateTime.TryParse(etv, out dt_edt))
                {
                    return "{\"success\" : false}";
                }

                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string k = "";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new NpgsqlCommand($"SELECT create_auction(@dt_sdt, @dt_edt);", connection);
                    command.Parameters.AddWithValue("dt_sdt", dt_sdt);
                    command.Parameters.AddWithValue("dt_edt", dt_edt);
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
        [Route("lots/increase/lid={lid:int}_uid={uid:int}_bid={bid:double}")]
        // api/auction/lots/increase/lid=4_uid=5_bid=15.498
        public string IncreaseBet(int lid, int uid, double bid)
        {
            try { 
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string k = "";
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new NpgsqlCommand($"SELECT increase_bid({lid}, {uid}, {bid.ToString().Replace(',', '.')});", connection);
                
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