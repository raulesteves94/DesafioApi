﻿using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Services;
using Microsoft.Owin.Security.OAuth;

namespace WebApplication1.Providers
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (PessoaLogin.Login(context.UserName, context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("acesso inválido", "As credenciais do usuário não conferem....");
                return;
            }
        }
    }
}