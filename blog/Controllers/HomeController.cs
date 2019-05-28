using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace blog.Controllers
{
    public class HomeController : Controller
    {
        public string PostsString => HttpContext.Session.GetString("Posts") ?? string.Empty;
        public bool HasPosts => !string.IsNullOrWhiteSpace(PostsString);
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult List()
        {
            var model = new BlogListViewModel();
            if (HasPosts)
            {
                var posts = JsonConvert.DeserializeObject<List<Blog>>(PostsString);
                model.BlogList = posts.Select(x => new BlogDetailViewModel(x)).ToList();
            }
            return PartialView(model);
        }
        public IActionResult Detail(int id)
        {
            var posts = JsonConvert.DeserializeObject<List<Blog>>(PostsString);
            var post = posts.SingleOrDefault(x => x.Id == id);
            if(post == null)
            {
                return RedirectToAction("Error");
            }
            var model = new BlogDetailViewModel(post);
            return View(model);
        }
        public IActionResult Create()
        {
            var model = new BlogCreateViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var blog = new Blog
                {
                    CreateDate = DateTime.Now,
                    Content = model.Content,
                    Title = model.Title,
                    ImageUrl = model.ImageUrl
                };
                var postsString = HttpContext.Session.GetString("Posts");
                List<Blog> posts;
                if (!string.IsNullOrWhiteSpace(postsString))
                {
                    posts = JsonConvert.DeserializeObject<List<Blog>>(postsString);
                    blog.Id = posts.Count + 1;
                    posts.Add(blog);
                }
                else
                {
                    blog.Id = 1;
                    posts = new List<Blog>
                    {
                        blog
                    };
                }
                HttpContext.Session.SetString("Posts", JsonConvert.SerializeObject(posts));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
