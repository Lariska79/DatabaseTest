using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Controllers
{
    public class MovieModel
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            var connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            connection.Open();

            var query = new SqlCommand("SELECT * FROM Movie");
            query.Connection = connection;
            var model = new Collection<MovieModel>();

           var reader =  query.ExecuteReader();
            while (reader.Read())
            {
                var item = new MovieModel();
                item.Name = Convert.ToString(reader["Name"]);
                item.ReleaseDate =Convert.ToDateTime(reader["ReleaseDate"]);
                model.Add(item);
            }
            return View(model);
        }
    }
}