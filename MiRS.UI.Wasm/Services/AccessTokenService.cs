using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MiRS.UI.Wasm.Gateway.Tokens;

namespace MiRS.UI.Wasm.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IAccessTokenProvider _tokenProvider;

        public AccessTokenService(IAccessTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            AccessTokenResult result = await _tokenProvider.RequestAccessToken();

            if (result.TryGetToken(out AccessToken token))
                return token.Value;

            return null;
        }
    }
}
