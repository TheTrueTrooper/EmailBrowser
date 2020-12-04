using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace EmailBrowser.Controllers
{
    
    public abstract class EmailAPIBaseController : Controller
    {
        public static string MyDomain { private set; get; } = "bythebooks-bc.ca";
        public static int MyDomainIMAPPort { private set; get; } = 143;

        [NonAction]
        protected ImapClient QLogin(string UserName, string Password)
        {
            return new ImapClient(MyDomain, MyDomainIMAPPort, UserName, Password, AuthMethod.Login, false);
        }

        [NonAction]
        protected ImapClient QLogin()
        {
            return new ImapClient(MyDomain, MyDomainIMAPPort, (string)Session["UserName"], (string)Session["Password"], AuthMethod.Login, false);
        }

        protected bool CheckLogin
        {
            get
            {
                if (Session["UserName"] != null && Session["Password"] != null)
                    return true;
                return false;
            }
        }

        [NonAction]
        protected JsonResult AccessDenied(JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "401", Message = "Access denied due to invalid credentials." }, RequestBehavior);
        }

        [NonAction]
        protected JsonResult AcceptedSuccess(JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "202", Message = "Success" }, RequestBehavior);
        }

        [NonAction]
        protected JsonResult CreatedSuccess(JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "201", Message = "Success" }, RequestBehavior);
        }

        [NonAction]
        protected JsonResult OkSuccess(JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "200", Message = "Success" }, RequestBehavior);
        }

        [NonAction]
        protected JsonResult OkSuccess(object Data, JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "200", Message = "Success", Data }, RequestBehavior);
        }

        [NonAction]
        protected JsonResult ConflictFail(string Conflict = "Description was not provided.", JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "409", Message = $"A conflict was detected for the following.\n{Conflict}" }, RequestBehavior);
        }

        [NonAction]
        protected JsonResult GoneFail(string Conflict = "Data has already been deleted.", JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "410", Message = $"Operation could not be completed for the following.\n{Conflict}" }, RequestBehavior);
        }

        const string AbortedOperation = "Operation aborted for the following.";
        [NonAction]
        protected JsonResult NotAllowedFail(string Conflict = "The method is restricted at this time.", JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "405", Message = $"{AbortedOperation}\n{Conflict}" }, RequestBehavior);
        }

        [NonAction]
        protected JsonResult ForbiddenFail(string Conflict = "The method is restricted to this level of access.", JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "403", Message = $"{AbortedOperation}\n{Conflict}" }, RequestBehavior);
        }

        [NonAction]
        protected JsonResult BadRequestFail(string Conflict = "The data provided was not properly formatted or contained an error.", JsonRequestBehavior RequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { Status = "400", Message = $"{AbortedOperation}\n{Conflict}" }, RequestBehavior);
        }
    }
}