using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailBrowser.Controllers
{
    [RoutePrefix("Mail")]
    public class MailBoxAPIController : EmailAPIBaseController
    {
        // GET: MailBox
        [HttpGet]
        [Route("GetMailBoxes")]
        public ActionResult GetMailBoxes()
        {
            if(CheckLogin)
            using (ImapClient Client = QLogin())
            {
                try
                { 
                    IEnumerable<string> MailBoxs = Client.ListMailboxes();
                    List<MailboxInfo> MailBoxes = new List<MailboxInfo>();
                    foreach(string MailBox in MailBoxs)
                            MailBoxes.Add(Client.GetMailboxInfo(MailBox));
                    return OkSuccess(MailBoxes);
                }
                catch(Exception e)
                {
                        return BadRequestFail(e.Message);
                }
            }
            return AccessDenied();
        }

        [HttpPost]
        [Route("CreateMailBox")]
        public ActionResult CreateNewMailBox(string NewBoxName)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        Client.CreateMailbox(NewBoxName);
                    }
                    catch(BadServerResponseException e)
                    {
                        return ConflictFail(e.Message);
                    }
                    catch (ArgumentNullException e)
                    {
                        return BadRequestFail(e.Message);
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                    return OkSuccess();
                }
            return AccessDenied();
        }

        [HttpPost]
        [Route("RenameMailBox")]
        public ActionResult RenameMailBox(string BoxName, string NewBoxName)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        Client.RenameMailbox(BoxName, NewBoxName);
                    }
                    catch (BadServerResponseException e)
                    {
                        return ConflictFail(e.Message);
                    }
                    catch (ArgumentNullException e)
                    {
                        return BadRequestFail(e.Message);
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                    return OkSuccess();
                }
            return AccessDenied();
        }

        [HttpPost]
        [Route("DeleteMailBox")]
        public ActionResult DeleteMailBox(string BoxToDeleteName)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        Client.DeleteMailbox(BoxToDeleteName);
                    }
                    catch (BadServerResponseException e)
                    {
                        return ConflictFail(e.Message);
                    }
                    catch (ArgumentNullException e)
                    {
                        return BadRequestFail(e.Message);
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                    return OkSuccess();
                }
            return AccessDenied();
        }
    }
}