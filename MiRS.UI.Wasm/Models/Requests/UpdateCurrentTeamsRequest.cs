using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Models.Requests
{
    public class UpdateCurrentTeamsRequest
    {
        public int EventId { get; set; }

        public IEnumerable<GuildTeam> CurrentTeamsToBeUpdated { get; set; } = Enumerable.Empty<GuildTeam>();
    }
}
