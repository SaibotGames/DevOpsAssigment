﻿using ent;

namespace inter;


public interface IPostRepository
{
    
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int postId);
    Task<Post> GetByIdAsync(int postId);
    IQueryable<Post> GetMany();
    
}