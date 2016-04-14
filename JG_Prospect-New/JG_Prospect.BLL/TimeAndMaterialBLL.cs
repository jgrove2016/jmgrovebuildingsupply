using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JG_Prospect.Common.modal;
using JG_Prospect.DAL;

namespace JG_Prospect.BLL
{
    public class TimeAndMaterialBLL
    {
        private static TimeAndMaterialBLL m_TimeAndMaterial = new TimeAndMaterialBLL();

        private TimeAndMaterialBLL()
        {
            //
        }

        public static TimeAndMaterialBLL Instance
        {
            get { return m_TimeAndMaterial;}
            set { ;}
        }

        public bool AddTimeAndMaterial(TimeAndMaterial timeAndMaterial)
        {
            return TimeAndMaterialDAL.Instance.AddTimeAndMaterial(timeAndMaterial);
        }

        public TimeAndMaterial GetTimeAndMaterial(TimeAndMaterial timeAndMaterial)
        {
            return TimeAndMaterialDAL.Instance.GetTimeAndMaterial(timeAndMaterial);
        }

    }
}
