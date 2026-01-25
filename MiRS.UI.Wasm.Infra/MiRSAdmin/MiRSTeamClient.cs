using Flurl.Http;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;

namespace MiRS.UI.Wasm.Infrastructure.MiRSAdmin
{
    public class MiRSTeamClient : IMiRSTeamClient
    {
        public async Task<IEnumerable<GuildTeam>> GetAllGuildTeams(long guildId)
        {
            GuildTeamContainer jsonResponse = await "https://localhost:7176/v1/"
            .WithHeader("Content-Type", "application/json")
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
