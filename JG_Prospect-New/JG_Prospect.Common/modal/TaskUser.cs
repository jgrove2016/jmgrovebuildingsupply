using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
  public  class TaskUser
    {
        public Int16 Mode;
        public Int64 Id;
        public Int64 TaskId;
        public Int32 UserId;
        public bool UserType;
        public Int16 Status;
        public string Notes;
        public bool UserAcceptance;
        public string UpdatedOn;
        public string Attachment;
    }
}
