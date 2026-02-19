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
        private readonly object _lock = new();

        private readonly BrowserStorageService _storage;
        private const string StorageKey = "eventsCache";

        public EventService(IMiRSEventClient mirsAdminClient, BrowserStorageService storage)
        {
            _mirsEventClient = mirsAdminClient;
            _storage = storage;
        }

        public Task LoadEvents()
        {
            lock (_lock)
            {
                if (_loadEvents == null)
                {
                    _loadEvents = LoadEventsInternal();
                    _loadEvents.ContinueWith(t =>
                    {
                        lock (_lock)
                        {
                            _loadEvents = null;
                        }
                    });
                }

                return _loadEvents;
            }
        }

        public async Task LoadEventsInternal()
        {
            IEnumerable<EventView> events = (await _mirsEventClient.GetAllEvents()).ToList();

            await _storage.SetToSessionStorage(StorageKey, events, TimeSpan.FromMinutes(10));
        }

        public async Task CreateNewGuildEvent(AddEventRequest addEventRequest)
        {
            await _mirsEventClient.CreateNewGuildEvent(new AddNewEventContainer
            {
                GuildId = addEventRequest.GuildId,
                Eventname = addEventRequest.Eventname,
                ParticipantPassword = addEventRequest.ParticipantPassword,
                EventPassword = addEventRequest.EventPassword,
                EventStart = addEventRequest.EventStart,
                EventEnd = addEventRequest.EventEnd,
            });
        }

        public async Task<bool> UpdateTeamsForEvent(UpdateCurrentTeamsRequest updateCurrentTeamsRequest)
        {

            await _mirsEventClient.UpdateGuildTeamsForEvent(new UpdateTeamList
            {
                EventId = updateCurrentTeamsRequest.EventId,
                CurrentTeamsToBeUpdated = updateCurrentTeamsRequest.CurrentTeamsToBeUpdated,
            });

            return true;
        }

        public async Task<bool> AddTeamsForEvent(AddTeamRequest addTeamRequest)
        {

            await _mirsEventClient.AddGuildTeamToEvent(new AddNewTeamToEventContainer
            {
                EventId = addTeamRequest.EventId,
                AddExistingTeamToggle = addTeamRequest.AddExistingTeamToggle,
                NewTeamToBeCreated = addTeamRequest.NewTeamToBeCreated,
            });

            return true;
        }

        public async Task<IList<GuildTeam>> GetTeamsFromEvent(int eventId)
        {
            return (await _mirsEventClient.GetGuildTeamsFromEvent(eventId)).ToList();

        }

        public async Task<IEnumerable<EventView>> GetAllEvent(bool force = false)
        {
            if (await _storage.GetFromSessionStorage<IEnumerable<EventView>>(StorageKey) is null || force)
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

        public async Task<bool> VerifyEventPassword(int eventId, ulong guildId, string eventpassword)
        {
            return await _mirsEventClient.VerifyEventPassword(eventId, guildId, eventpassword);
        }

    }
}
