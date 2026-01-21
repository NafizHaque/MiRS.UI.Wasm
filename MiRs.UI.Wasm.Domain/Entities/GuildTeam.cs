namespace MiRS.UI.Wasm.Domain.Entities
{
    public class GuildTeam
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public DateTimeOffset CreatedDate { get; set; }

        public IList<UserToTeam> UsersInTeam { get; set; } = new List<UserToTeam>();
    }
}
