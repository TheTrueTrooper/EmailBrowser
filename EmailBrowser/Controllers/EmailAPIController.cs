using EmailBrowser.Models;
using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace EmailBrowser.Controllers
{
    [RoutePrefix("Mail")]
    public class EmailAPIController : EmailAPIBaseController
    {
        [HttpGet]
        [Route("{FromMailBox}/GetReadEmails")]
        public ActionResult GetSeenEmails(string FromMailBox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        List<EmailMessage> Emails = new List<EmailMessage>();
                        IEnumerable<uint> BoxesMessageUDIs = Client.Search(SearchCondition.Seen(), FromMailBox);
                        //IEnumerable<MailMessage> MailBoxs = Client.GetMessages(BoxesMessageUDIs, mailbox: FromMailBox);

                        foreach (uint MessageUDI in BoxesMessageUDIs)
                            Emails.Add(new EmailMessage(MessageUDI, Client.GetMessage(MessageUDI, false, FromMailBox), Client.GetMessageFlags(MessageUDI)));
                        return OkSuccess(new { SeenEmails = Emails, SeenEmailCount = Emails.Count });
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpGet]
        [Route("{FromMailBox}/GetUnreadEmails")]
        public ActionResult GetUnseenEmails(string FromMailBox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        List<EmailMessage> Emails = new List<EmailMessage>();
                        IEnumerable<uint> BoxesMessageUDIs = Client.Search(SearchCondition.Unseen(), FromMailBox);
                        //IEnumerable<MailMessage> MailBoxs = Client.GetMessages(BoxesMessageUDIs, mailbox: FromMailBox);

                        foreach (uint MessageUDI in BoxesMessageUDIs)
                            Emails.Add(new EmailMessage(MessageUDI, Client.GetMessage(MessageUDI, false, FromMailBox), Client.GetMessageFlags(MessageUDI)));
                        return OkSuccess(new { UnseenEmails = Emails, UnseenEmailCount = Emails.Count });
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpGet]
        [Route("{FromMailBox}/GetAllEmails")]
        public ActionResult GetAllEmails(string FromMailBox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        List<EmailMessage> UnseenEmails = new List<EmailMessage>();
                        List<EmailMessage> SeenEmails = new List<EmailMessage>();
                        IEnumerable<uint> BoxesMessageUDIs = Client.Search(SearchCondition.Unseen(), FromMailBox);

                        //IEnumerable<MailMessage> MailBoxs = Client.GetMessages(BoxesMessageUDIs, mailbox: FromMailBox);

                        foreach (uint MessageUDI in BoxesMessageUDIs)
                            UnseenEmails.Add(new EmailMessage(MessageUDI, Client.GetMessage(MessageUDI, false), Client.GetMessageFlags(MessageUDI)));

                        BoxesMessageUDIs = Client.Search(SearchCondition.Seen(), FromMailBox);
                        //IEnumerable<MailMessage> MailBoxs = Client.GetMessages(BoxesMessageUDIs, mailbox: FromMailBox);

                        foreach (uint MessageUDI in BoxesMessageUDIs)
                            SeenEmails.Add(new EmailMessage(MessageUDI, Client.GetMessage(MessageUDI, false, FromMailBox), Client.GetMessageFlags(MessageUDI)));

                        return OkSuccess(new { UnseenEmails, UnseenEmailCount = UnseenEmails.Count, SeenEmails, SeenEmailCount = SeenEmails.Count });
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpGet]
        [Route("{Mailbox}/GetEmailAndMarkSeen/{EmailUDI}")]
        public ActionResult GetEmailAndMarkSeen(uint EmailUDI, string Mailbox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        EmailMessage Email = new EmailMessage(EmailUDI, Client.GetMessage(EmailUDI, mailbox: Mailbox), Client.GetMessageFlags(EmailUDI));

                        return OkSuccess(Email);
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpGet]
        [Route("{Mailbox}/DeleteEmail/{EmailUDI}")]
        public ActionResult DeleteEmail(uint EmailUDI, string Mailbox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        MessageFlag[] AddFlags = new MessageFlag[] { MessageFlag.Deleted };

                        Client.AddMessageFlags(EmailUDI, Mailbox, AddFlags);
                        //Client.DeleteMessage(EmailUDI);
                        Client.Expunge(Mailbox);
                        return OkSuccess();
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpPost]
        [Route("{Mailbox}/MoveEmail/{EmailUDI}")]
        public ActionResult MoveEmail(uint EmailUDI, string ToMailBox, string Mailbox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        Client.MoveMessage(EmailUDI, ToMailBox, Mailbox);

                        return OkSuccess();
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpPost]
        [Route("{Mailbox}/CopyEmail/{EmailUDI}")]
        public ActionResult CopyEmail(uint EmailUDI, string ToMailBox, string Mailbox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        Client.CopyMessage(EmailUDI, ToMailBox, Mailbox);

                        return OkSuccess();
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpPost]
        [Route("{FromMailBox}/SaveEmail")]
        public ActionResult SaveEmail(string FromMailBox, EmailMessage NewEmail)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        uint NewUDI = Client.StoreMessage(NewEmail, true, FromMailBox);

                        return OkSuccess();
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpPost]
        [Route("{Mailbox}/SetEmailFlags/{EmailUDI}")]
        public ActionResult SetEmailFlags(uint EmailUDI, EmailFlags EmailFlags, string Mailbox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        List<MessageFlag> AddFlags = new List<MessageFlag>();
                        List<MessageFlag> RemoveFlags = new List<MessageFlag>();
                        if (EmailFlags.Seen != null)
                            if (EmailFlags.Seen.Value == true)
                                AddFlags.Add(MessageFlag.Seen);
                            else
                                RemoveFlags.Add(MessageFlag.Seen);
                        if (EmailFlags.Deleted != null)
                            if (EmailFlags.Deleted.Value == true)
                                AddFlags.Add(MessageFlag.Deleted);
                            else
                                RemoveFlags.Add(MessageFlag.Deleted);
                        if (EmailFlags.Draft != null)
                            if (EmailFlags.Draft.Value == true)
                                AddFlags.Add(MessageFlag.Draft);
                            else
                                RemoveFlags.Add(MessageFlag.Draft);
                        if (EmailFlags.Recent != null)
                            if (EmailFlags.Recent.Value == true)
                                AddFlags.Add(MessageFlag.Recent);
                            else
                                RemoveFlags.Add(MessageFlag.Recent);
                        if (EmailFlags.Flagged != null)
                            if (EmailFlags.Flagged.Value == true)
                                AddFlags.Add(MessageFlag.Flagged);
                            else
                                RemoveFlags.Add(MessageFlag.Flagged);
                        if (EmailFlags.Answered != null)
                            if (EmailFlags.Answered.Value == true)
                                AddFlags.Add(MessageFlag.Answered);
                            else
                                RemoveFlags.Add(MessageFlag.Answered);
                        Client.AddMessageFlags(EmailUDI, Mailbox, AddFlags.ToArray());
                        Client.RemoveMessageFlags(EmailUDI, Mailbox, RemoveFlags.ToArray());
                        return OkSuccess();
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpGet]
        [Route("{Mailbox}/MarkEmailAsRead/{EmailUDI}")]
        public ActionResult MarkEmailAsRead(uint EmailUDI, string Mailbox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        MessageFlag[] AddFlags = new MessageFlag[] { MessageFlag.Seen };
                        
                        Client.AddMessageFlags(EmailUDI, Mailbox, AddFlags);
                        return OkSuccess();
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

        [HttpGet]
        [Route("{Mailbox}/MarkEmailAsUnread/{EmailUDI}")]
        public ActionResult MarkEmailAsUnread(uint EmailUDI, string Mailbox)
        {
            if (CheckLogin)
                using (ImapClient Client = QLogin())
                {
                    try
                    {
                        MessageFlag[] RemoveFlags = new MessageFlag[] { MessageFlag.Seen };

                        Client.RemoveMessageFlags(EmailUDI, Mailbox, RemoveFlags);
                        return OkSuccess();
                    }
                    catch (Exception e)
                    {
                        return BadRequestFail(e.Message);
                    }
                }
            return AccessDenied();
        }

    }
}