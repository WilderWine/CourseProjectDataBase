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
        public string GetUlog()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {
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

                return k;

            }
            catch (Exception ex)
            {
                return "Err";
            }
        }

        // GET api/users/userlog/1
        [HttpGet]
        [Route("userlog/{id:int}")]
        public string GetUlogId(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {
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
                return k;

            }
            catch(Exception e)
            {
                return "ERR";
            }
        }

        // GET api/users/notification/1
        [HttpGet]
        [Route("notifications/{id:int}")]
        public string GetNotificationsId(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {
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
                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        // GET api/users/check?login=ksdmfvjdfdkj

        [HttpGet]
        [Route("check")]
        public string CheckExists([FromUri] string login = null)
        {
            try { 
                if (login == null) return "{\"exists\" : false}";
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"exists\" : false}";
            }
        }

        // api/users/login?login=djfhbdf&password=skjhfb

        [HttpGet]
        [Route("login")]
        public string CheckExists([FromUri] string login = null, [FromUri]  string password = null)
        {
            try { 
                if (login == null || password == null) return "ERR";
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

                return k;
            }
            catch (Exception e)
            {
                return "ERR";
            }
        }

        // api/users/add?name=dfbdf&login=djfhbdf&password=skjhfb
        [HttpPost]
        [Route("add")]
        public string AddUser([FromUri] string name = null, [FromUri] string login = null, [FromUri] string password = null, [FromUri] int role = 2)
        {
            try { 
                if (login == null || password == null || name == null) return "{\"success\" : false}";
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        // POST api/users/delete/6
        [HttpPost]
        [Route("delete/{id:int}")]
        public string RemoveUser(int id)
        {
            try { 
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
                return k;
            }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        // POST api/users/resetname/0?data=bvd
        [HttpPost]
        [Route("resetname/{id:int}")]
        public string ResetName(int id, [FromUri] string data = null)
        {
            try {
                if (data == null) return  "{\"success\" : false}";
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
                return k;
            }
            catch (Exception e)
            {
                return  "{\"success\" : false}";
                }
        }

        // POST api/users/resetlogin/0?data=bvd
        [HttpPost]
        [Route("resetlogin/{id:int}")]
        public string ResetLogin(int id, [FromUri] string data)
        {
            try { 
                if (data == null) return "{\"success\" : false}";
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
                return k;
                }
            catch (Exception e)
            {
                return "{\"success\" : false}";
            }
        }

        // POST api/users/resetpassword/0?data=bvd&olddata=dnf
        [HttpPost]
        [Route("resetpassword/{id:int}")]
        public string ResetPassword(int  id,  [FromUri] string data, [FromUri] string olddata)
        {
            try { 
                if (data == null || olddata == null) return "{\"success\" : false}";
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
                return k;
            }
            catch (Exception e)
            {
                return  "{\"success\" : false}";
            }
        }

    }
}
