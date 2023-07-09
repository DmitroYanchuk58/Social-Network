using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RGR.Models;
using System.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace RGR.Controllers
{
    public class MainController : Controller
    {
        private readonly DbForRgrContext db = new();

        #region HttpGet

        [HttpGet]
        public IActionResult MyAccount(int id)
        {
            if (id >= 0)
            {
                return View(new AccountAndPostsVM(id));
            }
            else
            {
                var idCookie = Convert.ToInt32(Request.Cookies["id"]);
                return View(new AccountAndPostsVM(idCookie));
            }
        }


        [HttpGet]
        public IActionResult CreatePost(int id)
        {
            if (id >= 0)
            {
                return View(db.Accounts.Find(id));
            }
            else
            {
                var idCookie = Convert.ToInt32(Request.Cookies["id"]);
                return View(db.Accounts.Find(idCookie));
            }
        }

        [HttpGet]
        public IActionResult EditPost(int id) {
            if (id >=0 )
            {
                return View(db.Posts.Find(id));
            }
            else
            {
                var idCookie = Convert.ToInt32(Request.Cookies["id"]);
                return View(db.Posts.Find(idCookie));
            }
        }

        [HttpGet]
        public IActionResult EditAccount(int id) {
            if (id >= 0)
            {
                return View(new AccountAndPostsVM(id));
            }
            else
            {
                var idCookie = Convert.ToInt32(Request.Cookies["id"]);
                return View(new AccountAndPostsVM(idCookie));
            }
        }


        [HttpGet]
        public IActionResult Posts() => View(db.Posts.ToList());
        


        [HttpGet]
        public IActionResult Account(int idAccount) {
            var idCookie = Convert.ToInt32(Request.Cookies["id"]);
            AccountAndPostsVM data = new(idAccount);
            if (idAccount != idCookie)
            {
                return View(data);
            }
            else
            {
                return View("MyAccount", data);
            }
        }

        [HttpGet]
        public IActionResult AllAccounts() => View(db.Accounts.ToList());

        #endregion

        #region Post

        [HttpPost]
        public IActionResult EditPost(Post post, IFormFile image)
        {
            if (post.Description != null)
            {
                var idCookie = Convert.ToInt32(Request.Cookies["id"]);
                var editPost = db.Posts.Find(post.IdPost);
                if (editPost != null)
                {
                    if (image != null && image.Length != 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            image.CopyToAsync(memoryStream);
                            var imageData = memoryStream.ToArray();
                            editPost.Image = imageData;
                        }
                    }
                    editPost.Description = post.Description;
                    db.SaveChanges();
                    AccountAndPostsVM data = new((int)editPost.IdAccount);//can't be null because IdAccount`s required property Post
                    return View("MyAccount", data);
                }
                else { return View(db.Posts.Find(post.IdPost)); }
            }
            else
            {
                return View(db.Posts.Find(post.IdPost));
            }
        }

        [HttpPost]
        public IActionResult CreatePost([FromForm] string description, IFormFile photo)
        {
            var idCoockie = Convert.ToInt32(Request.Cookies["id"]);
            Post newPost = new Post() { IdAccount = idCoockie, Description = description, Time = DateTime.Now };
            if (photo != null && photo.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    photo.CopyTo(ms);
                    newPost.Image = ms.ToArray();
                }
            }
            newPost.IdPost=db.Posts.Count() == 0?0: db.Posts.Max(p => p.IdPost) + 1; 
            db.Posts.Add(newPost);
            db.SaveChanges();
            AccountAndPostsVM data = new(idCoockie);
            return View("MyAccount", data);
        }

        [HttpPost]
        public IActionResult EditAccount([FromForm] string description, IFormFile photo)
        {
            var idCoockie = Convert.ToInt32(Request.Cookies["id"]);
            var account = db.Accounts.Find(idCoockie);
            account.Description = description;
            if (photo != null && photo.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    photo.CopyTo(ms);
                    account.Avatarka = ms.ToArray();
                }
            }
            db.SaveChanges();
            AccountAndPostsVM data = new(idCoockie);
            return View("MyAccount", data);
        }

        [HttpPost]
        public IActionResult DeletePost(int idPost)
        {
            var idCoockie = Convert.ToInt32(Request.Cookies["id"]);
            var post = db.Posts.Find(idPost);
            db.Posts.Remove(post);
            db.SaveChanges();
            AccountAndPostsVM data = new(idCoockie);
            return View("MyAccount", data);
        }

        #endregion
    }
}
