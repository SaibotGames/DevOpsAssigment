using System.Text.Json;
using dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }
    [IgnoreAntiforgeryToken]
    [AllowAnonymous]
    public async Task<CreatePostDto> AddPostAsync(CreatePostDto post)
    {
        var json = JsonSerializer.Serialize(post);
        Console.WriteLine($"Sending JSON: {json}");  // Log the request body

        var response = await client.PostAsJsonAsync("posts/", post);
    
        Console.WriteLine($"Response Code: {response.StatusCode}");

        response.EnsureSuccessStatusCode();
        return post;
    }
[IgnoreAntiforgeryToken]
[AllowAnonymous]
    public async Task UpdatePostAsync(UpdatePostDto post, int id)
    {
        var response = await client.PutAsJsonAsync($"posts/{id}", post);
        response.EnsureSuccessStatusCode();
    }
    [IgnoreAntiforgeryToken]
    [AllowAnonymous]
    public async Task DeletePostAsync(int id)
    {
        var response = await client.DeleteAsync($"posts/{id}");
        response.EnsureSuccessStatusCode();
    }
    [IgnoreAntiforgeryToken]
    [AllowAnonymous]
    public async Task<PostDto> GetPostAsync(int id)
    {
        var post = await client.GetFromJsonAsync<PostDto>($"posts/{id}");
        return post!;
    }
    [IgnoreAntiforgeryToken]
    [AllowAnonymous]
    public Task<List<PostDto>> GetMany()
    {
        var posts = client.GetFromJsonAsync<List<PostDto>>("posts");
        return posts!;
    }
}