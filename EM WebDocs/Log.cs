using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;

namespace EM_WebDocs
{
    static class Log
    {
        private static string LOG_PATH = Directory.GetParent(Application.CommonAppDataPath).ToString() + "\\logs\\EMDocs_" + DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + ".log";

        public static void AddMessage(string message, string type)
        {
            if (!Directory.Exists(Directory.GetParent(Application.CommonAppDataPath).ToString() + "\\logs"))
            {
                Directory.CreateDirectory(Directory.GetParent(Application.CommonAppDataPath).ToString() + "\\logs");
            }

            using (StreamWriter file = new System.IO.StreamWriter(LOG_PATH, true))
            {
                file.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + type + "; " + message);
            }
        }
    }
}
