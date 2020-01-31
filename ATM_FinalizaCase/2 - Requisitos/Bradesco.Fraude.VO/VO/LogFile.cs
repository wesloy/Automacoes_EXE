using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bradesco.Fraude.VO.VO
{
    public class LogFile
    {
        public DateTime dataLog { get; set; }

        public string nameFile { get; set; }

        public string keyName { get; set; }
        public string walk { get; set; }
        public LogFile(string operacao,string pKeyName = "")
        {
            dataLog = DateTime.Now;
            nameFile = string.Concat(dataLog.Hour.ToString("00"), dataLog.Minute.ToString("00"), dataLog.Second.ToString("00"),"--", operacao, ".txt");
            nameFile = nameFile.Replace(":", "");
            keyName = pKeyName;
        }
    }
}
