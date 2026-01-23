using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Gateway.MiRSAdmin
{
    public interface IMiRSTeamClient
    {
        public Task<IEnumerable<GuildTeam>> GetAllGuildTeams(long guildId);
    }
}
