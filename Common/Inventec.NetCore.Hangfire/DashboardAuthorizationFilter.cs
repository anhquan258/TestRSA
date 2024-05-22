using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.Hangfire
{
    public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public IEnumerable<HangfireUserCredentials> Users { get; }
        private readonly bool isAllowAnonymous;

        public DashboardAuthorizationFilter(IEnumerable<HangfireUserCredentials> users, bool isAllowAnonymous)
        {
            Users = users;
            this.isAllowAnonymous = isAllowAnonymous;
        }

        public bool Authorize([NotNull] DashboardContext dashboardContext)
        {
            if (isAllowAnonymous)
            {
                return true;
            }
            var context = dashboardContext.GetHttpContext();

            string header = context.Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(header))
            {
                AuthenticationHeaderValue authValues = AuthenticationHeaderValue.Parse(header);

                if ("Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase))
                {
                    string parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));
                    var parts = parameter.Split(':');

                    if (parts.Length > 1)
                    {
                        string username = parts[0];
                        string password = parts[1];

                        if ((!string.IsNullOrWhiteSpace(username)) && (!string.IsNullOrWhiteSpace(password)))
                        {
                            return Users.Any(user => user.ValidateUser(username, password));
                        }
                    }
                }
            }

            return false;
        }
    }

    public class HangfireUserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("login");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            if (username == Username)
            {
                return password == Password;
            }
            else
                return false;
        }
    }
}
