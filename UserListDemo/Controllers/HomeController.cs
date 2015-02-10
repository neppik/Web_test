using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserListDemo.Models;

namespace UserListDemo.Controllers
{
    public class HomeController : Controller
    {

        public IEnumerable<UserModel> fill()
        {
            UserModel usr = new UserModel();
            usr.FName = "Peter";
            usr.LName = "Strieska";
            usr.Email = "peter.strieska@afsi.com";
            usr.Phone = "0915 111 222";
            usr.Age = 26;

            UserModel usr1 = new UserModel();
            usr1.FName = "Imre";
            usr1.LName = "Kutnas";
            usr1.Email = "imre.kutnas@afsi.com";
            usr1.Phone = "0907 000 999";
            usr1.Age = 30;

            UserModel usr2 = new UserModel();
            usr2.FName = "Janko";
            usr2.LName = "Mrkvicka";
            usr2.Email = "janko.mrkvicka@afsi.com";
            usr2.Phone = "0915 111 222";
            usr2.Age = 45;

            UserModel usr3 = new UserModel();
            usr3.FName = "Ivan";
            usr3.LName = "Cibula";
            usr3.Email = "ival.cibula@afsi.com";
            usr3.Phone = "0907 000 999";
            usr3.Age = 89;

            var list = new List<UserModel>();
            list.Add(usr);
            list.Add(usr1);
            list.Add(usr2);
            list.Add(usr3);


            return list;
        }

        public ActionResult Index()
        {
            var list = fill();

            return View(list);
        }

        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            var list = fill();

            return View(list);
        }

        [HttpGet]
        public ActionResult ShowUser(string usrID)
        {
            var list = fill();
            var tmp = new UserModel();
            var x = (List<UserModel>)list;

            foreach (UserModel localUsr in list)
            {
                if (localUsr.Email == usrID)
                {
                    tmp.FName = localUsr.FName;
                    tmp.LName = localUsr.LName;
                    tmp.Email = localUsr.Email;
                    tmp.Phone = localUsr.Phone;
                    tmp.Age = localUsr.Age;
                }
            }

            return View(tmp);
        }
    }
}
