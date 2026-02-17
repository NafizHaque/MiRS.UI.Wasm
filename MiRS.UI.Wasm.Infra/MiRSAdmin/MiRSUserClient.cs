using Flurl.Http;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;
using MiRS.UI.Wasm.Gateway.Tokens;

namespace MiRS.UI.Wasm.Infrastructure.MiRSAdmin
{
    public class MiRSUserClient : IMiRSUserClient
    {
        private readonly AppSettings _appSettings;

        private readonly IAccessTokenService _tokenService;

        public MiRSUserClient(IOptions<AppSettings> appSettings, IAccessTokenService tokenService)
        {
            _appSettings = appSettings.Value;
            _tokenService = tokenService;

        }

        public async Task<IEnumerable<GameUser>> GetUsersBySearch(string search, CancellationToken cancellationToken)
        {
            string token = await _tokenService.GetAccessTokenAsync();

            GameUserContainer jsonResponse = await _appSettings.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"user/")
                .WithOAuthBearerToken(token)
                .SetQueryParams(new
                {
                    search = search
                })
                .GetJsonAsync<GameUserContainer>(default, cancellationToken);

            return jsonResponse.Users;
        }
    }
}
