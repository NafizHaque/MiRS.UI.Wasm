namespace MiRS.UI.Wasm.Gateway.Tokens
{
    public interface IAccessTokenService
    {
        Task<string?> GetAccessTokenAsync();
    }
}
