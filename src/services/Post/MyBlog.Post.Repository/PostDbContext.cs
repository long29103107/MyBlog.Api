﻿using Microsoft.EntityFrameworkCore;
using MyBlog.Post.Domain;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository;

public class PostDbContext : DbContext
{
    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Entities.Post> Posts { get; set; }
    public virtual DbSet<Entities.Category> Categories { get; set; }
    public virtual DbSet<Entities.Comment> Comments { get; set; }
    public virtual DbSet<Entities.PostImage> PostImages { get; set; }
    public virtual DbSet<Entities.PostMetadata> PostMetadatas { get; set; }
    public virtual DbSet<Entities.Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(PostRepositoryReference.Assembly);
        base.OnModelCreating(modelBuilder);
    }
}