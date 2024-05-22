using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.AcsToken.Interfaces
{
    public interface IAcsUserContextService
    {
        string GetLoginname();

        string GetApplicationCode();

        string GetUsername();

        AcsTokenData GetAcsTokenData();

        AcsUserData GetAcsUserData();

        string GetTokenCode();

        string GetAcceptLanguage();
    }
}
