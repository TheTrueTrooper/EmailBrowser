using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace EmailBrowser.Models
{
    public class MailBox
    {
        public MailBox(MailboxInfo Box)
        {
            Name = Box.Name;
            FreeStorage = Box.FreeStorage;
            UsedStorage = Box.UsedStorage;
            MessageCount = Box.Messages;
            UnreadMessageCount = Box.Unread;
            Flags = Box.Flags;
        }

        public string Name { get; set; }
        public int UnreadMessageCount { get; set; }
        public int MessageCount { get; set; }
        public ulong FreeStorage { get; set; }
        public ulong UsedStorage { get; set; }
        public uint NextUID  { get; set; }
        public IEnumerable<MailboxFlag> Flags { get; set; }

        public Message[] Messages { get; set; }
    }
}