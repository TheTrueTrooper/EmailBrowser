using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace EmailBrowser.Models
{
    public class Dashboard
    {
        public MailBox[] MailBoxes { get; set; }
        public int? FocusedMailBoxNumber { get; set; } = null;
        public int? FocusedMessageNumber { get; set; } = null;

        public MailBox FocusedMailBox
        {
            get
            {
                if (FocusedMailBoxNumber != null)
                    return MailBoxes[FocusedMailBoxNumber.Value];
                return null;
            }
        }

        public Message FocusedMessage
        {
            get
            {
                if (FocusedMailBoxNumber != null && FocusedMessageNumber != null)
                    return MailBoxes[FocusedMailBoxNumber.Value].Messages[FocusedMessageNumber.Value];
                return null;
            }
        }
    }
}