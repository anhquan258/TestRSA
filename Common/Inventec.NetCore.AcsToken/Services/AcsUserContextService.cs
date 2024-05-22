using Inventec.NetCore.AcsToken.Authen;
using Inventec.NetCore.AcsToken.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.AcsToken.Services
{
    public class AcsUserContextService : IAcsUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AcsUserContextService> _logger;

        public AcsUserContextService(IHttpContextAccessor httpContextAccessor, ILogger<AcsUserContextService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public string GetAcceptLanguage()
        {
            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Accept-Language"))
                return _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
            else
                return null;
        }

        public AcsTokenData GetAcsTokenData()
        {
            AcsTokenData result = null;
            try
            {
                if (_httpContextAccessor?.HttpContext?.User != null
                    && _httpContextAccessor?.HttpContext?.User?.Identity != null
                    && _httpContextAccessor?.HttpContext?.User?.Identity.GetType() == typeof(AcsCredential))
                {
                    AcsCredential credential = (AcsCredential)_httpContextAccessor.HttpContext.User.Identity;
                    if (credential != null)
                    {
                        result = credential.Token;
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                _logger.LogError(ex, "Has exception when getAcsTokenData ");
            }
            return result;
        }

        public AcsUserData GetAcsUserData()
        {
            var tokenData = this.GetAcsTokenData();
            if (tokenData != null)
            {
                return tokenData.User;
            }
            return null;
        }

        public string GetApplicationCode()
        {
            var userData = this.GetAcsUserData();
            if (userData != null)
            {
                return userData.ApplicationCode;
            }
            return null;
        }

        public string GetLoginname()
        {
            var userData = this.GetAcsUserData();
            if (userData != null)
            {
                return userData.LoginName;
            }
            return null;
        }

        public string GetTokenCode()
        {
            var tokenData = this.GetAcsTokenData();
            if (tokenData != null)
            {
                return tokenData.TokenCode;
            }
            return null;
        }

        public string GetUsername()
        {
            var userData = this.GetAcsUserData();
            if (userData != null)
            {
                return userData.UserName;
            }
            return null;
        }
    }
}
