using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KursApi.Controllers
{
    //[RoutePrefix("api/users")]
    public class ValuesController : ApiController
    {
        // GET api/values
        
        public IEnumerable<string> Get()
        {
            string connectionString = "Host=localhost;Port=5433;Username=postgres;Password=ctrl_V_insert_0;Database=Lombard";

            string k = "";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();


                var command = new NpgsqlCommand("SELECT * FROM testtable;", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) // Добавьте этот цикл
                    {
                        // Получаем значения полей
                        string a = reader.GetString(1); // Индекс 1 для stringvalue
                        int b = reader.GetInt32(2);      // Индекс 2 для intvalue
                        k += $"{a}: {b}\t";
                    }
                }
            }

            return new string[] { k, k };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
