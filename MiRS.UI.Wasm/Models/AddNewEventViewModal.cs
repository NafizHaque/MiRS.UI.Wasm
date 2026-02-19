using System.ComponentModel.DataAnnotations;

namespace MiRS.UI.Wasm.Models
{
    public class AddNewEventViewModal
    {
        [Required(ErrorMessage = "Discord Server ID is required")]
        public ulong? GuildId { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        public string Eventname { get; set; } = string.Empty;

        public string ParticipantPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Event password is required")]
        public string EventPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Event Date is required")]
        public DateOnly EventStart { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1);

        [Required(ErrorMessage = "Event Time is required")]
        public int EventStartHour { get; set; } = 12;

        [Required(ErrorMessage = "Event Time is required")]
        public int EventStartMinutes { get; set; }

        [Required(ErrorMessage = "Event Time is required")]
        public string EventStartAmPm { get; set; } = "am";

        [Required(ErrorMessage = "Event Date is required")]
        public DateOnly EventEnd { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(2);

        [Required(ErrorMessage = "Event Time is required")]
        public int EventEndHour { get; set; } = 12;

        [Required(ErrorMessage = "Event Time is required")]
        public int EventEndMinute { get; set; }

        [Required(ErrorMessage = "Event Time is required")]

        public string EventEndAmPm { get; set; } = "am";
    }
}
