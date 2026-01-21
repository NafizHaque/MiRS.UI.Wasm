using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;

namespace MiRS.UI.Wasm.Services
{
    public class UsersService
    {
        private readonly IMiRSUserClient _mirsAdminClient;

        public UsersService(IMiRSUserClient mirsAdminClient)
        {
            _mirsAdminClient = mirsAdminClient;
        }

        public async Task<IList<UserToTeam>> UserSearch(string search, CancellationToken cancellationToken)
        {
            IEnumerable<GameUser> users = await _mirsAdminClient.GetUsersBySearch(search, cancellationToken);

            return users.Select(x => new UserToTeam
            {
                Id = 0,
                UserId = x.UserId,
                TeamId = 0,
                User = new GameUser
                {
                    UserId = x.UserId,
                    Username = x.Username,
                    PreviousUsername = x.PreviousUsername,
                    Runescapename = x.Runescapename,
                    PreviousRunescapename = x.PreviousRunescapename,
                    CreatedDate = x.CreatedDate,
                }
            }).ToList();
        }
    }
}
