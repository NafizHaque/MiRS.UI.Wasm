using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;
using MiRS.UI.Wasm.Models.Requests;

namespace MiRS.UI.Wasm.Services
{
    public class EventService
    {
        public List<EventView> Events { get; set; } = new();

        private readonly IMiRSEventClient _mirsAdminClient;
        private Task? _loadEvents;
        private readonly BrowserStorageService _storage;
        private const string StorageKey = "eventsCache";

        public EventService(IMiRSEventClient mirsAdminClient, BrowserStorageService storage)
        {
            _mirsAdminClient = mirsAdminClient;
            _storage = storage;
        }

        public Task LoadEvents()
        {
            if (_loadEvents is null)
            {
                _loadEvents = LoadEventsInternal();
            }
            return _loadEvents;
        }


        public async Task LoadEventsInternal()
        {
            var events = (await _mirsAdminClient.GetAllEvents()).ToList();

            // Save to generic cache
            await _storage.SetToSessionStorage(StorageKey, events, TimeSpan.FromMinutes(10));
        }


        public async Task<bool> UpdateTeamsForEvent(AddTeamRequest addTeamRequest)
        {

            await _mirsAdminClient.UpdateGuildTeamsForEvent(new UpdateTeamList
            {
                EventId = addTeamRequest.EventId,
                AddExistingTeamToggle = addTeamRequest.AddExistingTeamToggle,
                NewTeamToBeCreated = addTeamRequest.NewTeamToBeCreated,
                CurrentTeamsToBeUpdated = addTeamRequest.CurrentTeamsToBeUpdated,
            });

            return true;
        }

        public async Task<IList<GuildTeam>> GetTeamsFromEvent(int eventId)
        {
            return (await _mirsAdminClient.GetGuildTeamsFromEvent(eventId)).ToList();

        }

        public async Task<IEnumerable<EventView>> GetAllEvent()
        {
            if (await _storage.GetFromSessionStorage<IEnumerable<EventView>>(StorageKey) is null)
            {
                await LoadEvents();
            }

            var cachedEvents = await _storage.GetFromSessionStorage<IEnumerable<EventView>>(StorageKey);

            return cachedEvents;
        }

        public async Task<EventView?> GetEventById(int id)
        {
            if (await _storage.GetFromSessionStorage<IEnumerable<EventView>>(StorageKey) is null)
            {
                await LoadEvents();
            }

            var cachedEvents = await _storage.GetFromSessionStorage<IEnumerable<EventView>>(StorageKey);

            return cachedEvents.Where(e => e.Id == id).FirstOrDefault();
        }

        public async Task RemoveTeamFromEvent(int teamId, int eventId)
        {
            await _mirsAdminClient.RemoveTeamFromEvent(teamId, eventId);
        }

    }
}
