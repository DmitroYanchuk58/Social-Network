using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RGR.Models;

namespace RGR.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly DbForRgrContext db = new();

        #region HttpGet
        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Registration() => View();

        [HttpGet]
        public IActionResult Index()
        {
            var idCookie = Request.Cookies["id"];
            if (!string.IsNullOrEmpty(idCookie))
            {
                int idUser;
                if (int.TryParse(idCookie, out idUser))
                {
                    return RedirectToAction("MyAccount", "Main", new { id = idUser });
                }
            }

            return View("Registration");
        }

        #endregion

        #region HttpPost

        [HttpPost]
        public IActionResult Registration(Account account)
        {
            if (ModelState.IsValid)
            {
                if (!db.Accounts.Any(a => a.Email == account.Email))
                {
                    account.IdAccount = db.Accounts.Count() == 0 ? 0 : db.Accounts.Count();
                    db.Add(account);
                    db.SaveChanges();
                    return RedirectToAction("MyAccount", "Main", new { id = account.IdAccount });
                }
                else
                {
                    return View("Registration");
                }
            }
            else { return View(); }
        }

        [HttpPost]
        public IActionResult Login(Account account, bool Cookies)
        {
            var loginAccount = db.Accounts.FirstOrDefault(a => a.Email == account.Email && a.Password == account.Password);
            if (loginAccount!=null)
            {
                if (Cookies)
                {
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7) 
                    };
                    Response.Cookies.Append("id", loginAccount.IdAccount.ToString(),cookieOptions);
                }
                else
                {
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(10)
                    };
                    Response.Cookies.Append("id",loginAccount.IdAccount.ToString(), cookieOptions);
                }
                return RedirectToAction("MyAccount", "Main", new { id = loginAccount.IdAccount }) ;
            }
            else
            {
                return View();
            }
        }

        #endregion
    }

}
