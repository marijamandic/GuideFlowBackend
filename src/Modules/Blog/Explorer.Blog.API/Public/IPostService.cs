﻿using Explorer.Blog.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface IPostService
    {
        Result<PostDto> Create(PostDto post);
    }
}
