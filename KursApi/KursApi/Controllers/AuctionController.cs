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
        public List<Auction> GetAuctions()
        {
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

            try
            {
                List<Auction> auctions = JsonSerializer.Deserialize<List<Auction>>(k);
                return auctions;
            }
            catch (Exception e)
            {
                return new List<Auction>();
            }
        }

        [HttpGet]
        [Route("lots/{id:int}")]
        public Lot GetLotById(int id)
        {
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

            try
            {
                Lot lot = JsonSerializer.Deserialize<Lot>(k);
                return lot;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("lots/aid={aid:int}")]
        public List<Lot> GetLotsByAuction(int aid) 
        {
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

            try
            {
                List<Lot> lots = JsonSerializer.Deserialize<List<Lot>>(k);
                return lots;
            }
            catch (Exception e)
            {
                return new List<Lot>();
            }
        }

        [HttpGet]
        [Route("lots/uid={uid:int}")]
        public List<Lot> GetLotsByUser(int uid)
        {
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

            try
            {
                List<Lot> lots = JsonSerializer.Deserialize<List<Lot>>(k);
                return lots;
            }
            catch (Exception e)
            {
                return new List<Lot>();
            }
        }
    

        [HttpPost]
        [Route("add/sdtv={startdtv}/edtv={enddtv}")]
        public bool CreateAuction(String startdtv, String enddtv)
        {
            var sdt = startdtv.Split('_');
            string dtv = sdt[0] + " " + sdt[1].Replace('-', ':');
           
            var edt = enddtv.Split('_');
            string etv = edt[0] + " " + edt[1].Replace('-', ':');
            
            DateTime dt_sdt;
            if (!DateTime.TryParse(dtv, out dt_sdt))
            {
                return false;
            }
            DateTime dt_edt;
            if (!DateTime.TryParse(etv, out dt_edt))
            {
                return false;
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
        [Route("lots/increase/lid={lid:int}_uid={uid:int}_bid={bid:double}")]
        public bool IncreaseBet(int lid, int uid, double bid)
        {
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