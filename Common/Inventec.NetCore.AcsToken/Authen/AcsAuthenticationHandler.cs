using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Inventec.NetCore.AcsToken.Authen
{
    public class AcsAuthenticationHandler : AuthenticationHandler<AcsAuthenSchemeOptions>
    {
        private readonly string HeaderName = "TokenCode";
        private readonly AcsOptionConfig _acsOption;
        //private readonly ILogger<AcsAuthenticationHandler> _logger;

        public AcsAuthenticationHandler(IOptionsMonitor<AcsAuthenSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<AcsOptionConfig> acsOption)
            : base(options, logger, encoder, clock)
        {
            _acsOption = acsOption.Value;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.ContainsKey(HeaderName))
            {
                return AuthenticateResult.NoResult();
            }

            var authorizationHeader = Context.Request.Headers[HeaderName];
            if (!authorizationHeader.Any())
                return await Task.FromResult(AuthenticateResult.NoResult());

            var tokenCode = authorizationHeader.ToString();
            if (string.IsNullOrWhiteSpace(tokenCode))
                return await Task.FromResult(AuthenticateResult.NoResult());

            var credential = await this.GetCredentialAsync(tokenCode);
            if (credential == null)
            {
                return await Task.FromResult(AuthenticateResult.Fail("TokenCode invalid"));
            }


            // create a new claims identity and return an AuthenticationTicket 
            // with the correct scheme
            var claimsPrincipal = new ClaimsPrincipal(credential);
            var claimsIdentity = new ClaimsIdentity("AcsToken");
            claimsPrincipal.AddIdentity(claimsIdentity);

            var ticket = new AuthenticationTicket(claimsPrincipal, new AuthenticationProperties(), AcsAuthenSchemeOptions.Scheme);

            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private async Task<AcsCredential> GetCredentialAsync(string tokenCode)
        {
            AcsCredential rs = null;
            try
            {
                string address = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_acsOption.BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = new TimeSpan(0, 0, 300);
                    client.DefaultRequestHeaders.Add("TokenCode", tokenCode);
                    client.DefaultRequestHeaders.Add("Address", address);

                    HttpResponseMessage resp = await client.GetAsync(_acsOption.AuthenticateUri);
                    if (resp.IsSuccessStatusCode)
                    {
                        string responseData = await resp.Content.ReadAsStringAsync();
                        AcsAuthenticateResultData Data = JsonConvert.DeserializeObject<AcsAuthenticateResultData>(responseData);
                        if (Data != null && Data.Data != null && Data.Success)
                        {
                            rs = new AcsCredential(tokenCode);
                            rs.Token = Data.Data;
                        }
                        else
                        {
                            Logger.LogError($"Khong lay duoc tokenData theo token code: {tokenCode}, ipAddress: {address}, response: {Data}");
                        }
                    }
                    else
                    {
                        Logger.LogError($"Khong lay duoc tokenData theo token code: {tokenCode}, ipAddress: {address}, statusCode: {resp.StatusCode}, baseUrl: {_acsOption.BaseUrl}, authenUri: {_acsOption.AuthenticateUri}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Co exception khi goi api lay tokenData theo tokenCode: {tokenCode}, baseUrl: {_acsOption.BaseUrl}, authenUri: {_acsOption.AuthenticateUri}");
                rs = null;
            }
            return rs;
        }
    }
}
