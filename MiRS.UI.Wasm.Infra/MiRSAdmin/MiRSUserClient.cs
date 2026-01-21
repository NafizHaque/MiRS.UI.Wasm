using Flurl.Http;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;

namespace MiRS.UI.Wasm.Infrastructure.MiRSAdmin
{
    public class MiRSUserClient : IMiRSUserClient
    {
        public async Task<IEnumerable<GameUser>> GetUsersBySearch(string search, CancellationToken cancellationToken)
        {
            GameUserContainer jsonResponse = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment($"user/")
                .SetQueryParams(new
                {
                    search = search
                })
                .GetJsonAsync<GameUserContainer>(default, cancellationToken);

            return jsonResponse.Users;
        }
    }
}
