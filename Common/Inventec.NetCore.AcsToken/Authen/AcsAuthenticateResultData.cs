using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.AcsToken.Authen
{
    public class AcsAuthenticateResultData
    {
        public AcsTokenData Data { get; set; }
        public ResultParam Param { get; set; }
        public bool Success { get; set; }
    }
}
