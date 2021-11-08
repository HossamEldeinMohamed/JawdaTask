using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JawdaTask.DTO;
using JawdaTask.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JawdaTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuth _auth;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UserManager<IdentityUser> UserManager;
      
        public AccountController(IAuth auth , UserManager<IdentityUser> _UserManager, SignInManager<IdentityUser> _signInManager)
        {
            _auth = auth;

            SignInManager = _signInManager;
            UserManager = _UserManager;
        }

        // GET: LoginController
        public ActionResult Login()
        {
            return View();
        }

        // GET: LoginController/Details/5
        public ActionResult Register()
        {
            return View();
        }

       
        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginDTO loginDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (await _auth.Login(loginDTO))
        //            return RedirectToAction("Index", "Home");

        //        ViewBag.error = "User Name Or Passwored Is Incorrrect";
        //        return View();
        //    }
        //    else
        //        return View();
        //}
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                //var result = await SignInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, loginDTO.RememberMe, false);
                //if (result.Succeeded)
                //{

                //    return RedirectToAction("Index", "Home");
                //}
                //ModelState.AddModelError(string.Empty, "Invalid");

                if(await _auth.Login(loginDTO))
                    return RedirectToAction("Index", "Home");

            }
            return View(loginDTO);
        }
        public async Task<IActionResult> Logout()
        {
            await _auth.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                if (await _auth.Register(model))
                    return RedirectToAction("Index", "Home");

                ViewBag.error = "Please Ckeck Your Data And Try Again";

                return View();
            }
            return View();
        }

       
    }
}
