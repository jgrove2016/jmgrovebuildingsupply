using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JG_Prospect.Common
{
    public class AppConfiguration
    {
        private static string m_DomainName = null;

        public static string DomainName
        {
            get
            {
                if (m_DomainName == null)
                    m_DomainName = ConfigurationManager.AppSettings["URL"];

                return m_DomainName;
            }
        }
    }
}
