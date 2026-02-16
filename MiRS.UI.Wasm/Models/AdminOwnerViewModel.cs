using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Models
{
    public class AdminOwnerViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
    }
}
