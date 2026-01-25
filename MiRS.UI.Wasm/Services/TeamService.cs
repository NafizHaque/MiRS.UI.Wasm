using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;

namespace MiRS.UI.Wasm.Services
{
    public class TeamService
    {
        private readonly IMiRSTeamClient _mirsTeamClient;

        public TeamService(IMiRSTeamClient mirsTeamClient)
        {
            _mirsTeamClient = mirsTeamClient;
        }

        public async Task<IList<GuildTeam>> GetAllGuildTeams(long guildId)
        {
            return (await _mirsTeamClient.GetAllGuildTeams(guildId)).ToList();
        }
    }
}
