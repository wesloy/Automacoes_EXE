using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bradesco.Fraude.VO.VO
{
    public class MensageSms
    {

        public string uri { get; set; }
        public string attendanceTypeId { get; set; }
        public string userId { get; set; }
        public Fields field { get; set; }
        public Fields field2 { get; set; }

        public AuthenticationToken authenticationToken { get; set; }

        private string bodyMessage;

        public string getBodyMessage
        {
            get
            {
                this.bodyMessage = "{\"attendanceTypeId\":\"{0}\",\"userId\":\"{1}\",\"fields\":[{\"fieldId\":\"{2}\",\"value\":\"{3}\"},{\"fieldId\":\"{4}\",\"value\":\"{5}\"}]}".Replace("{0}", this.attendanceTypeId).Replace("{1}", this.userId).Replace("{2}", this.field.fieldId).Replace("{3}", this.field.value).Replace("{4}", this.field2.fieldId).Replace("{5}", this.field2.value);
                return bodyMessage;
            }
            private set { bodyMessage = value; }
        }
    }

    public class Fields
    {
        public string fieldId { get; set; }
        public string value { get; set; }
    }
}
