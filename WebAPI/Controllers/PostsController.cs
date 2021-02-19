using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [SwaggerOperation(Summary = "Retrieves all posts")]
        [HttpGet]
        public IActionResult Get()
        {
            var posts = _postService.GetAllPosts();
            return Ok(posts);
        }

        [SwaggerOperation(Summary = "Retrieves post by spefic id")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _postService.GetPostById(id);
            if(post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }


        [SwaggerOperation(Summary = "Retrieves post by a specific string")]
        [HttpGet("Search/{title}")]
        public IActionResult GetByString(string title)
        {
            var posts = _postService.GetPostByPhrase(title);

            if(posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
            
        }



        [SwaggerOperation(Summary = "Create a new post")]
        [HttpPost]
        public IActionResult Post(CreatePostDto newPost)
        {
            var post = _postService.AddNewPost(newPost);
            return Created($"api/posts/{post.Id}", post);
        }


        [SwaggerOperation(Summary = "Update a existing post")]
        [HttpPut]

        public IActionResult Update(UpdatePostDto updatedPost)
        {
            _postService.UpdatePost(updatedPost);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete existing post")]
        [HttpDelete]


        public IActionResult Delete(int id)
        {
            _postService.DeletePost(id);

            return NoContent();
        }
    }
}
