using DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

namespace KursUI.Services
{
    public class ApiService
    {
        private string BASE_URL;
        HttpClient client;
        public ApiService() 
        {
            BASE_URL = "https://localhost:44300/api/";
            client = new HttpClient();
        }

        #region Users

        public async Task<List<UselLog>> GetUlogAsync()
        {
            try
            {
                string? url = BASE_URL + "users/userlog";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<UselLog> userLogs = JsonSerializer.Deserialize<List<UselLog>>(json);
                return userLogs;
            }
            catch (Exception e)
            {
                return new List<UselLog>();
            }
        }

        public async Task<List<UselLog>> GetUlogIdAsync(int id)
        {
            try
            {
                string? url = BASE_URL + $"users/userlog/{id}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<UselLog> userLogs = JsonSerializer.Deserialize<List<UselLog>>(json);
                return userLogs;
            }
            catch (Exception e)
            {
                return new List<UselLog>();
            }
        }

        public async Task<List<Notification>> GetNotificationsId(int id)
        {
            try
            {
                string? url = BASE_URL + $"users/notifications/{id}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");
                List<Notification> notifications = JsonSerializer.Deserialize<List<Notification>>(json);
                return notifications;
            }
            catch (Exception e)
            {
                return new List<Notification>();
            }
        }
                
        public async Task<bool> CheckExistsAsync(string login)
        {
            try
            {
                string? url = BASE_URL + $"users/check?login={login}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult exists = JsonSerializer.Deserialize<BoolResult>(json);
                return exists.exists;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<User> CheckExistsAsync(string login, string password)
        {
            try
            {
                string? url = BASE_URL + $"users/login?login={login}&password={password}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                User user = JsonSerializer.Deserialize<User>(json);
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> AddUserAsync(string name,string login, string password, int role = 2)
        {
            try
            {
                string? url = BASE_URL + $"users/add?name={name}&login={login}&password={password}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");
                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            try
            {
                string? url = BASE_URL + $"users/delete/{id}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ResetNameAsync(int id, string data)
        {
            try
            {
                string? url = BASE_URL + $"users/resetname/{id}?data={data}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ResetLoginAsync(int id, string data)
        {
            try
            {
                string? url = BASE_URL + $"users/resetlogin/{id}?data={data}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(int id, string data, string olddata)
        {
           try
            {
                string? url = BASE_URL + $"users/resetpassword/{id}?data={data}&olddata={olddata}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion


        #region Products


        // api/products
        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                string? url = BASE_URL + $"products";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Product> products = JsonSerializer.Deserialize<List<Product>>(json);
                return products;
            }
            catch (Exception e)
            {
                return new List<Product>();
            }
        }

        // api/products/categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                string? url = BASE_URL + $"products/categories";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Category> categories = JsonSerializer.Deserialize<List<Category>>(json);
                return categories;
            }
            catch (Exception e)
            {
                return new List<Category>();
            }
        }

        // api/products?u_id=5
        public async Task<List<Product>> GetProductsByUserAsync( int u_id = 1)
        {
            try
            {
                string? url = BASE_URL + $"products?u_id={u_id}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Product> products = JsonSerializer.Deserialize<List<Product>>(json);
                return products;
            }
            catch (Exception e)
            {
                return new List<Product>();
            }
        }


        // api/products?status=hhh
        public async Task<List<Product>> GetProductsByStatusAsync( String status)
        {
            try
            {
                string? url = BASE_URL + $"products?status={status}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Product> products = JsonSerializer.Deserialize<List<Product>>(json);
                return products;
            }
            catch (Exception e)
            {
                return new List<Product>();
            }
        }

        // api/products?category=5
        public async Task<List<Product>> GetProductsByCategoryAsync( int category)
        {
            try
            {
                string? url = BASE_URL + $"products?category={category}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Product> products = JsonSerializer.Deserialize<List<Product>>(json);
                return products;
            }
            catch (Exception e)
            {
                return new List<Product>();
            }
        }

        // api/products?lot_id=1
        public async Task<Product> GetProductByLotAsync( int lot_id)
        {
            try
            {
                string? url = BASE_URL + $"products?lot_id={lot_id}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                Product products = JsonSerializer.Deserialize<Product>(json);
                return products;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        // api/products/5
        public async Task<Product> GetProductAsync(int id)
        {
            try
            {
                string? url = BASE_URL + $"products/{id}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                Product product = JsonSerializer.Deserialize<Product>(json);
                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        // api/products/delete/5
        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                string? url = BASE_URL + $"products/delete/{id}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        // api/products/confiscate/1?u_id=1
        public async Task<bool> ConfiscateProductAsync(int id,  int u_id = 1)
        {
            try
            {
                string? url = BASE_URL + $"products/confiscate/{id}?u_id={u_id}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/products/offer/5
        public async Task<bool> OfferProductAsync(int id)
        {
            try
            {
                string? url = BASE_URL + $"products/offer/{id}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/products/send/15_aid=1_bet=122.222
        public async Task<bool> SendToAuctionAsync(int id, int aid, double bet)
        {
            try
            {
                string? url = BASE_URL + $"products/send/{id}_aid={aid}_bet={bet.ToString().Replace(',', '.')}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/products/add?u_id=1&cat_id=1&name=prprpr&description=fignyakakayato
        public async Task<bool> AddProductAsync( int u_id = 1,  int cat_id = 1,  String status = "normal",
             String name = null,  String description = null)
        {
            try
            {
                string? url = BASE_URL + $"products/add?u_id={u_id}&cat_id={cat_id}&name={name}&description={description}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion


        #region Deals


        // api/deals?status=fjdbdj
        public async Task<List<Deal>> GetDealsByStatusAsync(String status)
        {
            try
            {
                string? url = BASE_URL + $"deals?status={status}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Deal> deals = JsonSerializer.Deserialize<List<Deal>>(json);
                return deals;
            }
            catch (Exception e)
            {
                return new List<Deal>();
            }
        }


        // api/deals?uid=15
        public async Task<List<Deal>> GetDealsByUserAsync(int uid)
        {
            try
            {
                string? url = BASE_URL + $"deals?uid={uid}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Deal> deals = JsonSerializer.Deserialize<List<Deal>>(json);
                return deals;
            }
            catch (Exception e)
            {
                return new List<Deal>();
            }
        }


        // api/deals/8
        public async Task<Deal> GetDealAsync(int did)
        {
            try
            {
                string? url = BASE_URL + $"deals/{did}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                Deal deal = JsonSerializer.Deserialize<Deal>(json);
                return deal;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        // api/deals/accept/11_number=dbudjdbnjhdbf
        public async Task<bool> AcceptDealAsync(int did, String number)
        {
            try
            {
                string? url = BASE_URL + $"deals/accept/{did}_number={number}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/deals/close/5_number=sjknjsbnksjdnbjdsk

        public async Task<bool> CloseDealAsync(int did, String number)
        {
            try
            {
                string? url = BASE_URL + $"deals/close/{did}_number={number}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        // api/deals/reject/7
        public async Task<bool> RejectDealAsync(int did)
        {
            try
            {
                string? url = BASE_URL + $"deals/reject/{did}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/deals/reoffer/4
        public async Task<bool> ReofferDealAsync(int did)
        {
            try
            {
                string? url = BASE_URL + $"deals/reoffer/{did}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        // api/deals/expire/7
        public async Task<bool> ExpireDealAsync(int did)
        {
            try
            {
                string? url = BASE_URL + $"deals/expire/{did}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        // api/deals/upd/8_debt=12.157
        public async Task<bool> SetDealDebtAsync(int did, double debt)
        {
            try
            {
                string? url = BASE_URL + $"deals/upd/{did}_debt={debt.ToString().Replace(',','.')}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        // api/deals/upd/8_loan=12.157
        public async Task<bool> SetDealLoanAsync(int did, double loan)
        {
            try
            {
                string? url = BASE_URL + $"deals/upd/{did}_loan={loan.ToString().Replace(',', '.')}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        // api/deals/upd/8/2024-10-15_12-15-15
        public async Task<bool> SetDealEndtermAsync(int did, DateTime dtv)
        {
            try
            {
                string esdtv = dtv.ToString("yyyy-MM-dd_HH-mm-ss");
                string? url = BASE_URL + $"deals/upd/{did}/{esdtv}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/deals/add?pid=5&uid=5&enddtv=2024-10-15_13-55-56&debt=56.65&loan=56.123
        public async Task<bool> CreateDealAsync(int pid, int uid, DateTime dtv, double debt, double loan)
        {
            try
            {
                string enddtv = dtv.ToString("yyyy-MM-dd_HH-mm-ss");
                string? url = BASE_URL + $"deals/add?pid={pid}&uid={uid}&enddtv={enddtv}&debt={debt.ToString().Replace(',','.')}&loan={loan.ToString().Replace(',','.')}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/deals/check
        public async Task<bool> CheckExpiredAsync()
        {
            try
            {
                string? url = BASE_URL + $"deals/check";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion


        #region Cards

        // GET api/cards/1

        public async Task<List<Card>> GetCardsAsync(int u_id)
        {
            try
            {
                string? url = BASE_URL + $"cards?u_id={u_id}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Card> userLogs = JsonSerializer.Deserialize<List<Card>>(json);
                return userLogs;
            }
            catch (Exception e)
            {
                return new List<Card>();
            }
        }

        
        // api/cards/deduct?number=dfbdjfbdjb&value=45.4
        public async Task<bool> DeductFromCardAsync(string number, double? value)
        {
            try
            {
                string? url = BASE_URL + $"cards/deduct?number={number}&value={value.ToString().Replace(',','.')}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        
        // api/cards/credit?number=dfbdjfbdjb&value=45.4
        public async Task<bool> CreditToCardAsync(string number , double? value)
        {
            try
            {
                string? url = BASE_URL + $"cards/credit?number={number}&value={value.ToString().Replace(',', '.')}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/cards/add?number=dfbdjfbdjb&u_id=1&remain=5645.58
        public async Task<bool> AddUserAsync(string number, int u_id, double remain = 0)
        {
            try
            {
                string? url = BASE_URL + $"cards/add?number={number}&u_id={u_id}&remain={remain.ToString().Replace(',', '.')}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion


        #region Auction

        // api/auction

        public async Task<List<Auction>> GetAuctionsAsync()
        {
           try
           {
                string? url = BASE_URL + $"auction";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Auction> auctions = JsonSerializer.Deserialize<List<Auction>>(json);
                return auctions;
            }
            catch (Exception e)
            {
                return new List<Auction>();
            }
        }

        // api/auction/lots/2
        public async Task<Lot> GetLotByIdAsync(int id)
        {
            try
            {
                string? url = BASE_URL + $"api/auction/lots/{id}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                Lot lot = JsonSerializer.Deserialize<Lot>(json);
                return lot;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // api/auction/lots/aid=12

        public async Task<List<Lot>> GetLotsByAuctionAsync(int aid)
        {
            try
            {
                string? url = BASE_URL + $"auction/lots/aid={aid}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Lot> lots = JsonSerializer.Deserialize<List<Lot>>(json);
                return lots;
            }
            catch (Exception e)
            {
                return new List<Lot>();
            }
        }

        // api/auction/lots/uid=2

        public async Task<List<Lot>> GetLotsByUserAsync(int uid)
        {
            try
            {
                string? url = BASE_URL + $"auction/lots/uid={uid}";
                var r = client.GetAsync(url).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                List<Lot> lots = JsonSerializer.Deserialize<List<Lot>>(json);
                return lots;
            }
            catch (Exception e)
            {
                return new List<Lot>();
            }
        }


        // api/auction/add/sdtv=2024-11-11_12-12-55/edtv=2024-11-11_12-12-55

        public async Task<bool> CreateAuctionAsync(DateTime sdtv, DateTime edtv)
        {
            try
            {
                string ssdtv = sdtv.ToString("yyyy-MM-dd_HH-mm-ss");
                string sedtv = edtv.ToString("yyyy-MM-dd_HH-mm-ss");
                string? url = BASE_URL + $"auction/add/sdtv={ssdtv}/edtv={sedtv}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/auction/lots/increase/lid=4_uid=5_bid=15.498
        public async Task<bool> IncreaseBetAsync(int lid, int uid, double bid)
        {
            try
            {
                string? url = BASE_URL + $"auction/lots/increase/lid={lid}_uid={uid}_bid={bid.ToString().Replace(',', '.')}";
                var r = client.PostAsync(url, null).Result;
                var s = await r.Content.ReadAsStringAsync();
                string json = s.Trim('"').Replace("\\\"", "\"");

                BoolResult success = JsonSerializer.Deserialize<BoolResult>(json);
                return success.success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion


    }
}
