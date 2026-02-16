namespace MiRS.UI.Wasm.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsCollapsed { get; set; } = false;

        public IList<Level>? Levels { get; set; } = new List<Level>();
    }
}
