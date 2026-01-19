using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TrilhaApiDesafio.Front.Models;

namespace TrilhaApiDesafio.Front.Services;

public class TodoApiClient
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public TodoApiClient(HttpClient http) => _http = http;

    private static async Task<T?> ReadJsonOrNull<T>(HttpResponseMessage res)
    {
        if (res.StatusCode == HttpStatusCode.NoContent) return default;
        return await res.Content.ReadFromJsonAsync<T>(JsonOptions);
    }

    public async Task<List<Todo>> FindAllAsync()
    {
        var res = await _http.GetAsync("Todo/FindAll");
        if (res.StatusCode == HttpStatusCode.NotFound) return new();
        res.EnsureSuccessStatusCode();
        return (await ReadJsonOrNull<List<Todo>>(res)) ?? new();
    }

    public async Task<Todo?> FindByIdAsync(int id)
    {
        var res = await _http.GetAsync($"Todo/{id}");
        if (res.StatusCode == HttpStatusCode.NotFound) return null;
        res.EnsureSuccessStatusCode();
        return await ReadJsonOrNull<Todo>(res);
    }

    public async Task<Todo> CreateAsync(Todo todo)
    {
        var res = await _http.PostAsJsonAsync("Todo", todo, JsonOptions);
        res.EnsureSuccessStatusCode();
        return (await ReadJsonOrNull<Todo>(res))!;
    }

    public async Task<Todo> UpdateAsync(int id, Todo todo)
    {
        var res = await _http.PutAsJsonAsync($"Todo/{id}", todo, JsonOptions);
        res.EnsureSuccessStatusCode();
        return (await ReadJsonOrNull<Todo>(res))!;
    }

    public async Task DeleteAsync(int id)
    {
        var res = await _http.DeleteAsync($"Todo/{id}");
        res.EnsureSuccessStatusCode();
    }
}