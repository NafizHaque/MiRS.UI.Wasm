namespace MiRS.UI.Wasm.Domain.Dtos
{
    public class AddNewEventContainer
    {
        public ulong? GuildId { get; set; }

        public string Eventname { get; set; } = string.Empty;

        public string ParticipantPassword { get; set; } = string.Empty;

        public string EventPassword { get; set; } = string.Empty;

        public DateTimeOffset EventStart { get; set; }

        public DateTimeOffset EventEnd { get; set; }
    }
}
