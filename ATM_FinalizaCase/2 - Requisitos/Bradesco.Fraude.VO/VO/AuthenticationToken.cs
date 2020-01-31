using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bradesco.Fraude.VO.VO
{
    public class AuthenticationToken
    {
        public String access_token { get; set; }
        public String token_type { get; set; }
        public String expires_in { get; set; }
        public DateTime dateToken { get; set; }

    }
}
