using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;
using MiRS.UI.Wasm.Models.Requests;

namespace MiRS.UI.Wasm.Services
{
    public class EventService
    {
        public List<EventView> Events { get; set; } = new();

        private readonly IMiRSEventClient _mirsEventClient;
        private Task? _loadEvents;
        private readonly BrowserStorageService _storage;
        private const string StorageKey = "eventsCache";

        public EventService(IMiRSEventClient mirsAdminClient, BrowserStorageService storage)
        {
            _mirsEventClient = mirsAdminClient;
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
            IEnumerable<EventView> events = (await _mirsEventClient.GetAllEvents()).ToList();

            await _storage.SetToSessionStorage(StorageKey, events, TimeSpan.FromMinutes(10));
        }

        public async Task<bool> UpdateTeamsForEvent(AddTeamRequest addTeamRequest)
        {

            await _mirsEventClient.UpdateGuildTeamsForEvent(new UpdateTeamList
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
            return (await _mirsEventClient.GetGuildTeamsFromEvent(eventId)).ToList();

        }

        public async Task<IEnumerable<EventView>> GetAllEvent()
        {
            if (await _storage.GetFromSessionStorage<IEnumerable<EventView>>(StorageKey) is null)
            {
                await LoadEvents();
            }

            IEnumerable<EventView> cachedEvents = await _storage.GetFromSessionStorage<IEnumerable<EventView>>(StorageKey);

            return cachedEvents;
        }

        public async Task<EventView?> GetEventById(int id)
        {
            if (await _storage.GetFromSessionStorage<IEnumerable<EventView>>(StorageKey) is null)
            {
                await LoadEvents();
            }

            IEnumerable<EventView> cachedEvents = await _storage.GetFromSessionStorage<IEnumerable<EventView>>(StorageKey);

            return cachedEvents.Where(e => e.Id == id).FirstOrDefault();
        }

        public async Task RemoveTeamFromEvent(int teamId, int eventId)
        {
            await _mirsEventClient.RemoveTeamFromEvent(teamId, eventId);
        }

    }
}
