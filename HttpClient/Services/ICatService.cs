using HttpClient.Models;

namespace HttpClient.Services;

public interface ICatService
{
    Task<IReadOnlyList<FactModel>> GetFacts();
}
