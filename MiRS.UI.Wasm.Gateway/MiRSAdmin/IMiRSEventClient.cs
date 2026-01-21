using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Gateway.MiRSAdmin
{
    public interface IMiRSEventClient
    {
        public Task<IEnumerable<GuildTeam>> GetGuildTeamsFromEvent(int eventId);

        public Task UpdateGuildTeamsForEvent(UpdateTeamList updateTeamList);

        public Task<IEnumerable<EventView>> GetAllEvents();

        public Task RemoveTeamFromEvent(int teamId, int eventId);

    }
}
