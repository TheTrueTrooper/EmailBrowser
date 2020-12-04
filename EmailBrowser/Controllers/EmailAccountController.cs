using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmailBrowser.Models;
using S22.Imap;
using System.Net.Mail;

namespace EmailBrowser.Controllers
{
    [RoutePrefix("Mail")]
    public class EmailAccountController : EmailAPIBaseController
    {
        [Route("Login")]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (ImapClient Client = QLogin(login.UserName, login.Password))
                    {
                        Session["UserName"] = login.UserName;
                        Session["Password"] = login.Password;
                        return RedirectToAction("Dashboard");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Error = "Login failed - " + e;
                }
            }
            return View("Login", login);
        }

        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            if (!CheckLogin)
                return RedirectToAction("Login", "Mail");
            return View();
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            using (ImapClient Client = QLogin())
                Client.Logout();
            Session["UserName"] = null;
            Session.Abandon();
            return View("Login");
        }
    }
}