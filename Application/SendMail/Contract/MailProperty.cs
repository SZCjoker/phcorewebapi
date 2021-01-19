using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.SendMail.Contract
{
 public  class MailProperty
    {
        public string Address { get; set; }

        public string Title { get; set; }

        public List<Attachment> Attachment { get; set; }
    }

    public class Attachment
    {
        public AttachmentType Type { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }


    public enum AttachmentType
    {
        pic,
        document
    }


}
