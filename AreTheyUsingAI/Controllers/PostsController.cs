using AreTheyUsingAI.Models;
using AreTheyUsingAI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AreTheyUsingAI.Controllers
{
    public class PostsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;
        private readonly DbService<Post> _postService;
        public PostsController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("AreTheyUsingAILocalDB");
            _postService = new PostService(_connectionString);
        }
        // GET: PostsController
        public ActionResult Index()
        {
            var commentService = new CommentService(_connectionString);
            var imageService = new ImageService(_connectionString);
            var posts = _postService.Get();
            foreach(var post in posts)
            {
                post.Comments = commentService.Get(post.Id);
                post.Images = imageService.Get(post.Id);
            }

            return View(posts);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Post newPost = new Post();
                //{
                //    PostTitle =
                //}
                collection.TryGetValue("PostTitle", out var title);
                collection.TryGetValue("PostDesc", out var description);
                newPost.PostTitle = title;
                newPost.PostDesc = description;
                var output = _postService.Post(newPost);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
