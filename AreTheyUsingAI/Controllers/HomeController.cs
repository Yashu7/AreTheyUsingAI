using AreTheyUsingAI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AreTheyUsingAI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;
        private List<Post> Posts { get; set; } = new List<Post>();

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("AreTheyUsingAILocalDB");
            ExecuteQuery();
        }
        public void ExecuteQuery()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM POST", connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var newPost = new Post();
                            newPost.Id = Convert.ToInt64(reader["Id"]);
                            newPost.PostTitle = reader["PostTitle"].ToString();
                            newPost.PostDesc = reader["PostDesc"].ToString();
                            newPost.ThumbsUp = Convert.ToInt32(reader["ThumbsUp"]);
                            newPost.ThumbsDown = Convert.ToInt32(reader["ThumbsDown"]);
                            Posts.Add(newPost);
                        }
                    }
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(Posts.First());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
