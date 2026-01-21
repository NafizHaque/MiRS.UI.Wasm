namespace MiRS.UI.Wasm.Domain.Dtos
{
    public class EventViewContainer
    {
        public IEnumerable<EventView> GuildEvents { get; set; } = Enumerable.Empty<EventView>();
    }
}
