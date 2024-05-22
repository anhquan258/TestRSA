using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore
{
    public class AcsTokenData
    {
        public string TokenCode { get; set; }
        public string RenewCode { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public string LoginAddress { get; set; }
        public AcsUserData User { get; set; }
        public string VersionApp { get; set; }
        public string MachineName { get; set; }
        public DateTime LastAccessTime { get; set; }
        public string AuthorSystemCode { get; set; }
        public string AuthenticationCode { get; set; }
        public List<AcsRoleData> RoleDatas { get; set; }
    }
}
