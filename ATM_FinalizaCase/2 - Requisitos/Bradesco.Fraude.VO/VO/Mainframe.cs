using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bradesco.Fraude.VO.VO
{
    public class Mainframe
    {
        public string user { get; set; }
        public string password { get; set; }
        
        private string userHomologa;
        public string getUserHomologa
        {
            get { return string.IsNullOrEmpty(userHomologa) ? user : userHomologa; }
            set { userHomologa = value; }
        }

        private string passwordHomologa;

        public string getPasswordHomologa
        {
            get { return string.IsNullOrEmpty(passwordHomologa)? password : passwordHomologa;}
            set { passwordHomologa = value; }
        }


      //  public string walk { get; set; }

       // public string walk
       // {
       //     get { return walk; }
       //     set { walk =  }
       //
       // }
        private string _walk;

        public string walk
        {
            get { return _walk; }
            set { _walk = string.Concat(@"\\",
                                       Environment.MachineName,
                                       @"\c$",
                                       @"\",
                                       value); }
            }

        public string numberApplication { get; set; }
        private LogFile log;

        public LogFile LogSistema
        {
            get { return log; }
            set { log = value; }
        }
    }
}
