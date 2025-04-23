namespace frontend.Services;

public interface IPostService
{
    Task<CreatePostDto> AddPostAsync(CreatePostDto post);
    Task UpdatePostAsync(UpdatePostDto post, int id);
    Task DeletePostAsync(int id);
    Task<PostDto> GetPostAsync(int id);
    Task<List<PostDto>> GetMany();
    
}