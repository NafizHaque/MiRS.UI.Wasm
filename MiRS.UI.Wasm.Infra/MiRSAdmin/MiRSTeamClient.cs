using Flurl.Http;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;
using MiRS.UI.Wasm.Gateway.Tokens;

namespace MiRS.UI.Wasm.Infrastructure.MiRSAdmin
{
    public class MiRSTeamClient : IMiRSTeamClient
    {
        private readonly AppSettings _appSettings;

        private readonly IAccessTokenService _tokenService;

        public MiRSTeamClient(IOptions<AppSettings> appSettings, IAccessTokenService tokenService)
        {
            _appSettings = appSettings.Value;
            _tokenService = tokenService;

        }

        public async Task<IEnumerable<GuildTeam>> GetAllGuildTeams(long guildId)
        {
            string token = await _tokenService.GetAccessTokenAsync();

            GuildTeamContainer jsonResponse = await _appSettings.BaseUrl
            .WithHeader("Content-Type", "application/json")
            .WithOAuthBearerToken(token)
            .AppendPathSegment($"Teams/")
            .SetQueryParams(new
            {
                guildid = guildId,
            })
            .GetJsonAsync<GuildTeamContainer>();

            return jsonResponse.GuildTeams;
        }
    }
}
