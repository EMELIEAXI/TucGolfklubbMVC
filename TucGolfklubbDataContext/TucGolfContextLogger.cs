using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace TucGolfklubb.DataContext.SqlServer
{
        public class TucGolfContextLogger
        {
            public static void WriteLine(string message)
            {
                string path = Path.Combine(GetFolderPath(SpecialFolder.DesktopDirectory), "tucgolflog.txt");

                StreamWriter textFile = File.AppendText(path);
                textFile.WriteLine(message);
                textFile.Close();
            }

        }
}
