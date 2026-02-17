using Flurl.Http;
using Microsoft.Extensions.Options;
using MiRs.Domain.Configurations;
using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;
using MiRS.UI.Wasm.Gateway.Tokens;

namespace MiRS.UI.Wasm.Infrastructure.MiRSAdmin
{
    public class MiRSOwnerClient : IMiRSOwnerClient
    {
        private readonly AppSettings _appSettings;

        private readonly IAccessTokenService _tokenService;

        public MiRSOwnerClient(IOptions<AppSettings> appSettings, IAccessTokenService tokenService)
        {
            _appSettings = appSettings.Value;
            _tokenService = tokenService;

        }

        public async Task<IEnumerable<Category>> GetGameMetadata()
        {
            string token = await _tokenService.GetAccessTokenAsync();

            CategoryContainer jsonResponse = await _appSettings.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment($"runehunter/metadata/")
                .GetJsonAsync<CategoryContainer>();

            return jsonResponse.Categories;
        }

        public async Task UpdateGameMetadata(CategoryContainer metadataToBeUpdated)
        {
            string token = await _tokenService.GetAccessTokenAsync();

            await _appSettings.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment($"runehunter/metadata/")
                .PostJsonAsync(metadataToBeUpdated.Categories);
        }
    }
}
