﻿using _2_Blog_CQRS.Common;
using _2_Blog_CQRS.Pipelines;

namespace _2_Blog_CQRS.Domain.Posts.Queries;

public record GetAllPosts : IQuery<PostDTO[]>;

[RetryPolicy(RetryCount = 3)]
public record GetDetailedPost(int Id) : IQuery<PostDetailDTO>;