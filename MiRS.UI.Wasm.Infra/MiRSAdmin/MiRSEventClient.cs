using Flurl.Http;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;
using MiRS.UI.Wasm.Gateway.Tokens;

namespace MiRS.UI.Wasm.Infrastructure.MiRSAdmin
{
    public class MiRSEventClient : IMiRSEventClient
    {
        private readonly IAccessTokenService _tokenService;

        private readonly AppSettings _appSettings;

        public MiRSEventClient(IOptions<AppSettings> appSettings, IAccessTokenService tokenService)
        {
            _appSettings = appSettings.Value;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<GuildTeam>> GetGuildTeamsFromEvent(int eventId)
        {
            string token = await _tokenService.GetAccessTokenAsync();

            GuildTeamContainer jsonResponse = await _appSettings.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
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
            string token = await _tokenService.GetAccessTokenAsync();

            await _appSettings.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment($"events/teamstoevent/")
                .PostJsonAsync(addNewTeamToEventContainer);
        }

        public async Task UpdateGuildTeamsForEvent(UpdateTeamList updateTeamList)
        {
            string token = await _tokenService.GetAccessTokenAsync();

            await _appSettings.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment($"events/teamstoevent/")
                .PatchJsonAsync(updateTeamList);
        }

        public async Task RemoveTeamFromEvent(int teamId, int eventId)
        {
            string token = await _tokenService.GetAccessTokenAsync();

            await _appSettings.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
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
            string token = await _tokenService.GetAccessTokenAsync();

            EventViewContainer jsonResponse = await _appSettings.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment($"events/allevents/")
                .GetJsonAsync<EventViewContainer>();

            return jsonResponse.GuildEvents;
        }

        public async Task<bool> VerifyEventPassword(int eventId, ulong guildId, string eventPassword)
        {
            UpdateEventVerificationContainer jsonResponse = await _appSettings.BaseUrl
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
