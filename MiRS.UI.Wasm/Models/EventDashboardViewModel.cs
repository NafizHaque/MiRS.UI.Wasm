using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Models
{
    public class EventDashboardViewModel
    {
        public IList<GuildTeam> Teams { get; set; } = new List<GuildTeam>();

        public bool ShowModal { get; set; }

        public bool IsEnabled { get; set; }

        public string TeamAdminUnlock { get; set; } = string.Empty;
    }
}
