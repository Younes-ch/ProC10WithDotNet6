﻿namespace AutoLot.Api.Security;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly SecuritySettings _securitySettings;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IOptionsMonitor<SecuritySettings> securitySettingsMonitor) : base(options, logger, encoder, clock)
    {
        _securitySettings = securitySettingsMonitor.CurrentValue;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var endpoint = Context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is not null)
        {
            return AuthenticateResult.NoResult();
            ;
        }

        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            string userName = credentials[0];
            string password = credentials[1];

            if (userName.Equals(_securitySettings.UserName, StringComparison.OrdinalIgnoreCase)
                && password.Equals(_securitySettings.Password, StringComparison.OrdinalIgnoreCase))
            {
                Claim[] claims =
                [
                    new(ClaimTypes.NameIdentifier, userName),
                    new(ClaimTypes.Name, userName)
                ];

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
}
