using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Domain.Dtos
{
    public class CategoryContainer
    {
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
    }
}
