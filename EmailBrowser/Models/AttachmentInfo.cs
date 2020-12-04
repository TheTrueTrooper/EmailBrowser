using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace EmailBrowser.Models
{
    public class AttachmentInfo
    {
        public AttachmentInfo(Attachment Attachment)
        {
            ContentID = Attachment.ContentId;
            Name = Attachment.Name;
            ContentType = Attachment.ContentType;
            NameEncoding = Attachment.NameEncoding?.EncodingName;
        }
        public string ContentID { get; set; }
        public string NameEncoding { get; set; }
        public string Name { get; set; }
        public ContentType ContentType { get; set; }
    }
}