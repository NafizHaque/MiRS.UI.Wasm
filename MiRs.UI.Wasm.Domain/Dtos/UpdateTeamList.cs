using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Domain.Dtos
{
    public class UpdateTeamList
    {
        public int EventId { get; set; }

        public bool AddExistingTeamToggle { get; set; } = false;

        public GuildTeam NewTeamToBeCreated { get; set; } = new GuildTeam();

        public IEnumerable<GuildTeam> CurrentTeamsToBeUpdated { get; set; } = Enumerable.Empty<GuildTeam>();
    }
}
