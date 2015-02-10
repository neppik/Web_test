using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserListDemo.Models;
using UserListDemo.Services;

namespace UserListDemo.Controllers
{
    public class UsersController : Controller
    {
        private UsersService userService;
        
        public UsersController() {
            userService = new UsersService();
        }
        
        public ActionResult Index()
        {
            var list = userService.GetUsers();
            return View(list);
        }

  

        [HttpGet]
        public ActionResult Update(int id)
        {
            return View(userService.GetUserById(id));
        }

        [HttpPost]
        public ActionResult Update(UserModel usr)
        {
            if (ModelState.IsValid)
            {
                userService.UpdateUser(usr);
                return RedirectToAction("Index", "Users");
            }
            return View(usr);
        }

        [HttpPost]
        public ActionResult Delete(UserModel usr)
        {
            userService.DeleteUser(usr);
            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserModel usr)
        {
            if (ModelState.IsValid)
            {
                userService.CreateUser(usr);
                return RedirectToAction("Index", "Users");
            }
            
            return View(usr);
        }

        public ActionResult ShowUser(int usrID)
        {
            var newUsr = userService.GetUserById(usrID);

            return View(newUsr);
        }
    }
}
