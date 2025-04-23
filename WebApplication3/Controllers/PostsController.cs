using dto;
using ent;
using inter;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers;

[IgnoreAntiforgeryToken]
[ApiController]
[Route("[controller]")]
public class PostsController
{
    private readonly IPostRepository repository;

    public PostsController(IPostRepository repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public async Task<IResult> CreatePost([FromBody] CreatePostDto post)
    {
        Console.WriteLine("WAS CALLED");
        Post newPost = new Post(post.PostTitle, post.PostContent);

        var added = await repository.AddAsync(newPost);
        return Results.Created($"posts/{added.Id}", added);
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdatePost(int id, [FromBody] UpdatePostDto post)
    {
        var postToUpdate = await repository.GetByIdAsync(id);
        postToUpdate.Content = post.PostContent ?? postToUpdate.Content;
        postToUpdate.Title = post.PostTitle ?? postToUpdate.Title;
        await repository.UpdateAsync(postToUpdate);
        return Results.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeletePost(int id)
    {
        await repository.DeleteAsync(id);
        return Results.NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetPost(int id)
    {
        var post = await repository.GetByIdAsync(id);
        return Results.Ok(MapToPostDto(post));
    }

    [HttpGet]
    public IResult GetAllPosts()
    {
        var posts = repository.GetMany().Select(MapToPostDto).ToList();
        return Results.Ok(posts);
    }

    private static PostDto MapToPostDto(Post post)
    {
        return new PostDto()
        {
            Id = post.Id,
            PostTitle = post.Title,
            PostContent = post.Content
        };
    }
}