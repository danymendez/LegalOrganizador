using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Utilidades
{
    public sealed class ExceptionUtility
    {
        private ExceptionUtility()
        { }

        public static void LogException(Exception Exc)
        {
            if (Exc == null)
                return;

            string FullPath = Path.GetFullPath("~/wwwroot/logs/index.html").Replace("~\\", "");
            string sEvent = string.Empty;

            string Hora = $"{DateTime.Now.ToString("dd")}/{DateTime.Now.ToString("MM")}/{DateTime.Now.Year} - {DateTime.Now.ToString("HH")}:{DateTime.Now.ToString("mm")}:{DateTime.Now.Second }";
            sEvent += Environment.NewLine + "=============================== " + Hora + " ===================================" + Environment.NewLine;

            if (Exc.GetType() != null)
                sEvent += "Exception Type:\t" + Exc.GetType().ToString() + Environment.NewLine;
            if (Exc.Message != null)
                sEvent += "Exception:\t" + Exc.Message + Environment.NewLine;
            if (Exc.Source != null)
                sEvent += "Source:\t\t" + Exc.Source + Environment.NewLine;
            if (Exc.StackTrace != null)
                sEvent += "Stack Trace:\t" + Exc.StackTrace + Environment.NewLine;

            if (Exc.InnerException != null)
            {
                if (Exc.InnerException.GetType() != null)
                    sEvent += "Inner Exception Type: " + Exc.InnerException.GetType().ToString() + Environment.NewLine;
                if (Exc.InnerException.Message != null)
                    sEvent += "Inner Exception:\t" + Exc.InnerException.Message + Environment.NewLine;
                if (Exc.InnerException.Source != null)
                    sEvent += "Inner Source:\t" + Exc.InnerException.Source + Environment.NewLine;
                if (Exc.InnerException.StackTrace != null)
                    sEvent += "Inner Stack Trace:\t" + Exc.InnerException.StackTrace + Environment.NewLine;
            }

            try
            {
                using (var str = new FileStream(FullPath, FileMode.Append, FileAccess.Write))
                {
                    if (str.CanWrite)
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(sEvent);
                        str.Write(info, 0, info.Length);
                    }
                }
            }
            catch
            {
                throw Exc;
            }
        }
    }
}
