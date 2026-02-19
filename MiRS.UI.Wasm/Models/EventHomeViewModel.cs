using MiRS.UI.Wasm.Domain.Dtos;

namespace MiRS.UI.Wasm.Models
{
    public class EventHomeViewModel
    {
        public IEnumerable<EventView> Events { get; set; } = Enumerable.Empty<EventView>();

        public bool ShowEventCreateModal { get; set; }
    }
}
