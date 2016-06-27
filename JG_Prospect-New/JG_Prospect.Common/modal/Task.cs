using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    public class Task
    {
        public UInt16 Mode;
        public UInt64 TaskId;
        public string Title;
        public string Description;
        public UInt16 Status;
        public string DueDate;
        public Int16 Hours;
        public string Notes;
        public string Attachment;
        public UInt32 CreatedBy;
        public string CreatedOn;
    }
}
