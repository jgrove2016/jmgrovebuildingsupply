using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JG_Prospect.Common.modal
{
    public class TaskUser
    {
        public UInt16 Mode;
        public UInt64 Id;
        public UInt64 TaskId; 
        public UInt32 UserId;
        public bool  UserType;
        public UInt16 Status;
        public string Notes;
        public bool UserAcceptance;
        public string UpdatedOn;
        public string Attachment;
    }
}
