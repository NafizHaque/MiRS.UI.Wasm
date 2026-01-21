using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Models
{
    public class AddTeamViewModel
    {
        public int EventId { get; set; }

        public bool AddExistingTeamToggle { get; set; } = false;

        public GuildTeam NewTeamToBeCreated { get; set; } = new GuildTeam();

        public IEnumerable<GuildTeam> CurrentTeamsToBeUpdated { get; set; } = Enumerable.Empty<GuildTeam>();

        public GuildTeam UnassignedUsers { get; set; } = new GuildTeam { Id = 0, TeamName = "Unassigned" };

        public int SelectedEditModeTeamId { get; set; }

        public CancellationTokenSource? UserSearchCancelToken { get; set; }
    }
}
