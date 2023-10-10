namespace DB.Models;

public class TokenGenerationRequest
{
    public string Email { get; set; }
    public Guid UserId { get; set; }
    public IEnumerable<KeyValuePair<string, string>> CustomClaims { get; set; }
}
