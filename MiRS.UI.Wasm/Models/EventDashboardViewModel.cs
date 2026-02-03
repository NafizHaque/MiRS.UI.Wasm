using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Models
{
    public class EventDashboardViewModel
    {
        public IList<GuildTeam> Teams { get; set; } = new List<GuildTeam>();

        public IList<GuildTeam> UnassignedGuildTeams { get; set; } = new List<GuildTeam>();

        public GuildTeam TeamToBeEditted { get; set; } = new GuildTeam();

        public bool ShowModalAdd { get; set; }

        public bool ShowModalEdit { get; set; }

        public bool IsEnabled { get; set; }

        public string TeamAdminUnlock { get; set; } = string.Empty;
    }
}
