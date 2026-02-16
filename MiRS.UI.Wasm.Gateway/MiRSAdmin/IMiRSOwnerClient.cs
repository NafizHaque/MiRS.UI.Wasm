using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Gateway.MiRSAdmin
{
    public interface IMiRSOwnerClient
    {
        public Task<IEnumerable<Category>> GetGameMetadata();

        public Task UpdateGameMetadata(CategoryContainer metadataToBeUpdated);
    }
}
