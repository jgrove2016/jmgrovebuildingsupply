using System;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace JG_Prospect.Common.Logger
{
    public class LogManager
    {
        private static LogManager m_LogManager = new LogManager();
        private static LogWriter m_LogWriter;
        private static Object m_Sync = new object();

        private LogManager()
        {
            
        }

        public static LogManager Instance
        {
            get { return m_LogManager; }
            private set { }
        }

        private LogWriter LogWriterInstance
        {
            get 
            {
                if (m_LogWriter == null)
                {
                    lock (m_Sync)
                    {
                        if (m_LogWriter == null)
                        {
                            m_LogWriter = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();
                        }
                    }
                }
                return m_LogWriter;
            } 
        }

        public void WriteToFlatFile(string message)
        {
            WriteToFlatFile(message, Category.General, Priority.Normal);
        }

        public void WriteToFlatFile(Exception ex)
        {
            WriteToFlatFile(ex.Message, Category.General, Priority.Normal);
        }
     
        public void WriteToFlatFile(string message, string category, int priority)
        {
            if (m_LogWriter == null)
                m_LogWriter = LogWriterInstance;

            if (!m_LogWriter.IsLoggingEnabled())
                return;

            LogEntry logEntry = new LogEntry();
            logEntry.Message = message;
            logEntry.Priority = priority;
            logEntry.Categories.Add(category);

            m_LogWriter.Write(logEntry);
        }

    }
}