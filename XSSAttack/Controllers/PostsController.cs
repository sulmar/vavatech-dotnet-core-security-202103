using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSSAttack.IServices;
using XSSAttack.Models;

namespace XSSAttack.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository postRepository;

        public PostsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        // GET: PostsController
        public ActionResult Index()
        {
            IEnumerable<Post> posts = postRepository.Get();

            return View(posts);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            Post post = postRepository.Get(id);

            return View(post);
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
            Post post = postRepository.Get(id);

            return View(post);
        }

        // POST: PostsController/Edit/5
        [HttpPost]        
        public ActionResult Edit([FromForm] Post post)
        {
            try
            {
                postRepository.Update(post);

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
