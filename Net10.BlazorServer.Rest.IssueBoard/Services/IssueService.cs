using System.Net.Http.Json;
using Shared.Rest.IssueBoard.Dtos;

namespace Net10.BlazorServer.Rest.IssueBoard.Services;

public class IssueService
{
    private readonly HttpClient _httpClient;

    public IssueService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<IssueDto>> GetIssuesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<IssueDto>>("api/issues") ?? [];
    }

    public async Task<IssueDto?> GetIssueAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<IssueDto>($"api/issues/{id}");
    }

    public async Task<IssueDto?> CreateIssueAsync(CreateIssueDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/issues", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IssueDto>();
    }

    public async Task<IssueDto?> UpdateIssueAsync(int id, UpdateIssueDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/issues/{id}", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IssueDto>();
    }

    public async Task DeleteIssueAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/issues/{id}");
        response.EnsureSuccessStatusCode();
    }
}
