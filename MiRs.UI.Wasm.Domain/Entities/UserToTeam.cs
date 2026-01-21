namespace MiRS.UI.Wasm.Domain.Entities
{
    public class UserToTeam
    {
        public int Id { get; set; }

        public ulong UserId { get; set; }

        public int TeamId { get; set; }

        public GameUser? User { get; set; }
    }
}
