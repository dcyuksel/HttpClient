using HttpClient.Models;

namespace HttpClient.Services;

public class CatService(System.Net.Http.HttpClient httpClient) : ICatService
{
    public async Task<IReadOnlyList<FactModel>> GetFacts()
    {
        var url = $"facts/random/?animal_type=cat&amount=5";
        var facts = await httpClient.GetFromJsonAsync<List<FactModel>>(url);

        return facts ?? [];
    }
}
