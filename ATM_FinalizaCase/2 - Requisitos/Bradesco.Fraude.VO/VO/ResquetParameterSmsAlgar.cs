using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Bradesco.Fraude.VO.VO
{
    public class ResquetParameterSmsAlgar
    {
        public string uri { get; set; }
        public string aplicationDesc { get; set; }
        public string username { get; set; }
        public string passWord { get; set; }
        private string gratType { get; set; }
        public NetworkCredential credential { get; set; }

        public string getGrantType
        {
            get { return "password"; }
            set { gratType = "password"; }
        }

        private string valueRequest;
        public string getValueRequest
        {
            get { return $"grant_type={getGrantType}&username={username}&password={passWord}"; }
            set { valueRequest = value; }
        }


    }
}
