using S22.Imap;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace EmailBrowser.Models
{
    public class EmailMessage
    {
        public EmailMessage(uint UDI, MailMessage Message, IEnumerable<MessageFlag> MessageFlags)
        {
            this.UDI = UDI;
            BodyEncoding = Message.BodyEncoding?.WebName;
            Body = Message.Body;
            IsBodyHtml = Message.IsBodyHtml;
            Sender = Message.Sender;
            From = Message.From;
            SubjectEncoding = Message.SubjectEncoding?.WebName;
            Subject = Message.Subject;
            ReplyToList = Message.ReplyToList;
            To = Message.To;
            CC = Message.CC;
            BCC = Message.Bcc;
            Priority = Message.Priority;
            HeadersEncoding = Message.HeadersEncoding?.WebName;
            Headers = new Dictionary<string, string>();
            DeliveryNotificationOptions = (int)Message.DeliveryNotificationOptions;
            foreach(string Key in Message.Headers.Keys)
                Headers.Add(Key, Message.Headers[Key]);
            Attachments = new List<AttachmentInfo>();
            foreach(Attachment Att in Message.Attachments)
                Attachments.Add(new AttachmentInfo(Att));
            this.MessageFlags = MessageFlags.ToArray();
        }

        public static implicit operator MailMessage(EmailMessage Message)
        {
            MailMessage Return = new MailMessage()
            {
                From = Message.From,
                Sender = Message.Sender,
                Body = Message.Body,
                Subject = Message.Subject,
                SubjectEncoding = Encoding.GetEncoding(Message.SubjectEncoding),
                BodyEncoding = Encoding.GetEncoding(Message.BodyEncoding),
                HeadersEncoding = Encoding.GetEncoding(Message.HeadersEncoding),
                DeliveryNotificationOptions = Message.DeliveryNotificationOptions != null ? (DeliveryNotificationOptions)Message.DeliveryNotificationOptions : System.Net.Mail.DeliveryNotificationOptions.None,
                Priority = Message.Priority,
                IsBodyHtml = Message.IsBodyHtml,
            };
            foreach (MailAddress Address in Message.ReplyToList)
            {
                Return.ReplyToList.Add(Address);
            }
            foreach (MailAddress Address in Message.BCC)
            {
                Return.Bcc.Add(Address);
            }
            foreach (MailAddress Address in Message.CC)
            {
                Return.CC.Add(Address);
            }
            foreach (MailAddress Address in Message.To)
            {
                Return.To.Add(Address);
            }
            return Return;
        }

        public uint? UDI { get; set; } = null;
        public bool IsBodyHtml { get; set; }
        public MailPriority Priority { get; set; }
        public MessageFlag[] MessageFlags;
        public MailAddress Sender { get; set; }
        public MailAddress From { get; set; }
        public MailAddressCollection ReplyToList { get; set; }
        public MailAddressCollection To { get; set; }
        public MailAddressCollection CC { get; set; }
        public MailAddressCollection BCC { get; set; }
        public string BodyEncoding { get; set; }
        public string Body { get; set; }
        public string SubjectEncoding { get; set; }
        public string Subject { get; set; }
        public int? DeliveryNotificationOptions { get; set; }
        public string HeadersEncoding { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public List<AttachmentInfo> Attachments { get; set; }
    }
}