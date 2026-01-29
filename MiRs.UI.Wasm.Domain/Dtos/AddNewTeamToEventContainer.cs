using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Domain.Dtos
{
    public class AddNewTeamToEventContainer
    {
        /// <summary>
        /// Gets or sets the EventId.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the AddExistingTeamToggle.
        /// </summary>
        public bool AddExistingTeamToggle { get; set; } = false;

        /// <summary>
        /// Gets or sets the NewTeamToBeCreated.
        /// </summary>
        public GuildTeam NewTeamToBeCreated { get; set; } = new GuildTeam();
    }
}
