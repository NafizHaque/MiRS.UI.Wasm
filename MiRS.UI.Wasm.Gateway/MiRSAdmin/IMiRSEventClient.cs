using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Gateway.MiRSAdmin
{
    public interface IMiRSEventClient
    {
        public Task CreateNewGuildEvent(AddNewEventContainer addNewEventContainer);

        public Task<IEnumerable<GuildTeam>> GetGuildTeamsFromEvent(int eventId);

        public Task UpdateGuildTeamsForEvent(UpdateTeamList updateTeamList);

        public Task AddGuildTeamToEvent(AddNewTeamToEventContainer addNewTeamToEventContainer);

        public Task<IEnumerable<EventView>> GetAllEvents();

        public Task RemoveTeamFromEvent(int teamId, int eventId);

        public Task<bool> VerifyEventPassword(int eventId, ulong guildId, string eventPassword);

    }
}
