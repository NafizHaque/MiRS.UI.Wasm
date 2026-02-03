using Flurl.Http;
using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;

namespace MiRS.UI.Wasm.Infrastructure.MiRSAdmin
{
    public class MiRSEventClient : IMiRSEventClient
    {
        public async Task<IEnumerable<GuildTeam>> GetGuildTeamsFromEvent(int eventId)
        {
            GuildTeamContainer jsonResponse = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"events/teamstoevent/")
                .SetQueryParams(new
                {
                    eventid = eventId
                })
                .GetJsonAsync<GuildTeamContainer>();

            return jsonResponse.GuildTeams;
        }

        public async Task AddGuildTeamToEvent(AddNewTeamToEventContainer addNewTeamToEventContainer)
        {
            await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"events/teamstoevent/")
                .PostJsonAsync(addNewTeamToEventContainer);
        }

        public async Task UpdateGuildTeamsForEvent(UpdateTeamList updateTeamList)
        {
            await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"events/teamstoevent/")
                .PatchJsonAsync(updateTeamList);
        }

        public async Task RemoveTeamFromEvent(int teamId, int eventId)
        {
            await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"events/teamstoevent/")
                .SetQueryParams(new
                {
                    eventid = eventId,
                    teamid = teamId
                })
                .DeleteAsync();
        }

        public async Task<IEnumerable<EventView>> GetAllEvents()
        {
            EventViewContainer jsonResponse = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"events/allevents/")
                .GetJsonAsync<EventViewContainer>();

            return jsonResponse.GuildEvents;
        }

        public async Task<bool> VerifyEventPassword(int eventId, ulong guildId, string eventPassword)
        {
            UpdateEventVerificationContainer jsonResponse = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"events/verify/")
                .SetQueryParams(new
                {
                    eventid = eventId,
                    guildid = guildId,
                    eventpassword = eventPassword,
                })
                .GetJsonAsync<UpdateEventVerificationContainer>();

            return jsonResponse.Verfied;
        }
    }
}
