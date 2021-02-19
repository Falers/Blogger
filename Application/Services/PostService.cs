using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postrepository, IMapper mapper)
        {
            _postRepository = postrepository;
            _mapper = mapper;
        }

        public PostDto AddNewPost(CreatePostDto newPost)
        {
            if(string.IsNullOrEmpty(newPost.Title))
            {
                throw new Exception("Post must have a title");
            }

            var post = _mapper.Map<Post>(newPost);
            _postRepository.Add(post);
            return _mapper.Map<PostDto>(post);

        }

        public void DeletePost(int id)
        {
            var post = _postRepository.GetById(id);

            _postRepository.Delete(post);
        }

        public IEnumerable<PostDto> GetAllPosts()
        {
            var posts = _postRepository.GetAll();

            return _mapper.Map<IEnumerable<PostDto>>(posts);

        }


        public PostDto GetPostById(int id)
        {
            var post = _postRepository.GetById(id);

            return _mapper.Map<PostDto>(post);
        }

        public IEnumerable<PostDto> GetPostByPhrase(string title)
        {
            var posts = _postRepository.GetByPhrase(title);

            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public void UpdatePost(UpdatePostDto updatedPost)
        {
            var existingPost = _postRepository.GetById(updatedPost.Id);
            var post = _mapper.Map(updatedPost, existingPost);
            _postRepository.Update(post);
        }
    }
}
