using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace EmailBrowser.Models
{
    public class Message
    {
        public uint? UDI { get; set; }
        public MailMessage TheMessage { get; set; }
    }
}