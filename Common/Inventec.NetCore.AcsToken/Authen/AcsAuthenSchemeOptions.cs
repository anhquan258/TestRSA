using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.AcsToken.Authen
{
    public class AcsAuthenSchemeOptions : AuthenticationSchemeOptions
    {
        public const string Scheme = "AcsToken";
    }
}
