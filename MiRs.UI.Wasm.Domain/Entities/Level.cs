namespace MiRS.UI.Wasm.Domain.Entities
{
    public class Level
    {
        public int Id { get; set; }

        public int Levelnumber { get; set; }

        public string Unlock { get; set; } = string.Empty;

        public string UnlockDescription { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public IList<LevelTask>? LevelTasks { get; set; } = new List<LevelTask>();
    }
}
