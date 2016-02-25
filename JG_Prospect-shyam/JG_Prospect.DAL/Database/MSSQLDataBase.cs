using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JG_Prospect.Common;
using JG_Prospect.DAL.Interface;

using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace JG_Prospect.DAL.Database
{
    public class MSSQLDataBase 
    {
        private static MSSQLDataBase m_SQLDataBase = new MSSQLDataBase();

        private MSSQLDataBase()
        {

        }

        public static MSSQLDataBase Instance
        {
            get { return m_SQLDataBase; }
            private set { ; }
        }

        public SqlDatabase GetDefaultDatabase()
        {
            return EnterpriseLibraryContainer.Current.GetInstance<SqlDatabase>(DBConstants.CONFIG_CONNECTION_STRING_KEY);
        }

    }
}
