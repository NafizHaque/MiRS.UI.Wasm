namespace MiRS.UI.Wasm.Models.Requests
{
    public class AddEventRequest
    {
        public ulong? GuildId { get; set; }

        public string Eventname { get; set; } = string.Empty;

        public string ParticipantPassword { get; set; } = string.Empty;

        public string EventPassword { get; set; } = string.Empty;

        public DateTimeOffset EventStart { get; set; }

        public DateTimeOffset EventEnd { get; set; }

    }
}
