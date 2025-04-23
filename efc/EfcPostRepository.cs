using ent;
using inter;
using inter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly ApplicationDbContext ctx;

    public EfcPostRepository(ApplicationDbContext ctx)
    {
        this.ctx = ctx;
    }
    
    public async Task<Post> AddAsync(Post post)
    {
        EntityEntry<Post> entry = await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p=>p.Id == post.Id)))
        {
            throw new KeyNotFoundException($"Post {post.Id} not found");
        }
        
        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int postId)
    {
        Post? existingPost = await ctx.Posts.SingleOrDefaultAsync(p=>p.Id == postId);
        if (existingPost == null)
        {
            throw new KeyNotFoundException($"Post {postId} not found");
        }
        
        ctx.Posts.Remove(existingPost);
        await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetByIdAsync(int postId)
    {
        Post? existingPost = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == postId);
        if (existingPost == null)
        {
            throw new KeyNotFoundException($"Post {postId} not found");
        }
        
        return existingPost;
    }

    public IQueryable<Post> GetMany()
    {
        return ctx.Posts.AsQueryable();
    }
}