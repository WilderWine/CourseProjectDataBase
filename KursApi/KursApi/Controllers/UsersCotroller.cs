using Microsoft.Ajax.Utilities;
using Npgsql;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using DbModels;
using System.Text.Json;
using System.Web.Helpers;
using System;

namespace KursApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {

        //GET api/users/userlog

        [HttpGet]
        [Route("userlog")]
        public List<UselLog> GetUlog()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT get_userlog_json();", connection);
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
                 List<UselLog> userLogs = JsonSerializer.Deserialize<List<UselLog>>(k);
                 return userLogs;
            }
            catch (Exception e)
            {
                return new List<UselLog>();
            }
           
        }

        // GET api/users/userlog/1
        [HttpGet]
        [Route("userlog/{id:int}")]
        public List<UselLog> GetUlogId(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();


                var command = new NpgsqlCommand($"SELECT get_userlog_json({id});", connection);
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
                List<UselLog> userLogs = JsonSerializer.Deserialize<List<UselLog>>(k);
                return userLogs;
            }
            catch (Exception e)
            {
                return new List<UselLog>();
            }
        }

        // GET api/users/notification/1
        [HttpGet]
        [Route("notifications/{id:int}")]
        public List<Notification> GetNotificationsId(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT get_notifications_json({id});", connection);
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
                List<Notification> notifications = JsonSerializer.Deserialize<List<Notification>>(k);
                return notifications;
            }
            catch (Exception e)
            {
                return new List<Notification>();
            }
        }

        // GET api/users/check?login=ksdmfvjdfdkj

        [HttpGet]
        [Route("check")]
        public bool CheckExists([FromUri] string login = null)
        {
            if (login == null) return false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();


                var command = new NpgsqlCommand($"SELECT check_user_exists('{login}');", connection);
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
                BoolResult exists = JsonSerializer.Deserialize<BoolResult>(k);
                return exists.exists;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // api/users/login?login=djfhbdf&password=skjhfb

        [HttpGet]
        [Route("login")]
        public User CheckExists([FromUri] string login = null, [FromUri]  string password = null)
        {
            if (login == null || password == null) return null;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT get_user_data('{login}', '{password}');", connection);
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
                User user = JsonSerializer.Deserialize<User>(k);
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // api/users/add?name=dfbdf&login=djfhbdf&password=skjhfb
        [HttpPost]
        [Route("add")]
        public bool AddUser([FromUri] string name = null, [FromUri] string login = null, [FromUri] string password = null, [FromUri] int role = 2)
        {
            if (login == null || password == null || name == null) return false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT create_user('{name}','{login}', '{password}', {role});", connection);
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

        // POST api/users/delete/6
        [HttpPost]
        [Route("delete/{id:int}")]
        public bool RemoveUser(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT delete_user({id});", connection);
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

        // POST api/users/resetname/0?data=bvd
        [HttpPost]
        [Route("resetname/{id:int}")]
        public bool ResetName(int id, [FromUri] string data = null)
        {
            if (data == null) return false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT set_new_name({id}, '{data}');", connection);
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

        // POST api/users/resetlogin/0?data=bvd
        [HttpPost]
        [Route("resetlogin/{id:int}")]
        public bool ResetLogin(int id, [FromUri] string data)
        {
            if (data == null) return false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT set_new_login({id}, '{data}');", connection);
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

        // POST api/users/resetpassword/0?data=bvd&olddata=dnf
        [HttpPost]
        [Route("resetpassword/{id:int}")]
        public bool ResetPassword(int  id,  [FromUri] string data, [FromUri] string olddata)
        {
            if (data == null || olddata == null) return false;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand($"SELECT set_new_password({id}, '{olddata}', '{data}');", connection);
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
