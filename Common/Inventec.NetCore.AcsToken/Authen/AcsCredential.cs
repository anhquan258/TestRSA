using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.AcsToken.Authen
{
    public class AcsCredential : GenericIdentity
    {
        public AcsTokenData Token { get; set; }
        public string ApplicationCode { get; set; }
        public AcsCredential(string name) : base(name) { }
    }
}
