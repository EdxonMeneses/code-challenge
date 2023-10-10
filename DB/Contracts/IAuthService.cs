using DB.Models;

namespace DB.Contracts;

public interface IAuthService
{
    public string BuildToken(TokenGenerationRequest tokenGenerationRequest);
}
