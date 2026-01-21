using MiRS.UI.Wasm.Domain.Entities;

namespace MiRS.UI.Wasm.Gateway.MiRSAdmin
{
    public interface IMiRSUserClient
    {
        public Task<IEnumerable<GameUser>> GetUsersBySearch(string search, CancellationToken cancellationToken);

    }
}
