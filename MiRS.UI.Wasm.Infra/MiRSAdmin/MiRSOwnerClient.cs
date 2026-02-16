using Flurl.Http;
using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;

namespace MiRS.UI.Wasm.Infrastructure.MiRSAdmin
{
    public class MiRSOwnerClient : IMiRSOwnerClient
    {
        public async Task<IEnumerable<Category>> GetGameMetadata()
        {
            CategoryContainer jsonResponse = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"runehunter/metadata/")
                .GetJsonAsync<CategoryContainer>();

            return jsonResponse.Categories;
        }

        public async Task UpdateGameMetadata(CategoryContainer metadataToBeUpdated)
        {
            await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"runehunter/metadata/")
                .PostJsonAsync(metadataToBeUpdated.Categories);
        }
    }
}
