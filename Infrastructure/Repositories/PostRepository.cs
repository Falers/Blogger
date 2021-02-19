using Application.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {

   

        private static readonly ISet<Post> _posts = new HashSet<Post>()
        {
            new Post(1, "Jak zostać programistą", "..."),
            new Post(2, "Ile zarabia programista", "..."),
            new Post(3, "Dlaczego warto zostać programistą", "..."),
            new Post(4, "programistą", "..."),
        };
        public IEnumerable<Post> GetAll()
        {
            return _posts;
        }

        public Post GetById(int id)
        {
            return _posts.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Post> GetByPhrase(string title)
        {
            var posts = _posts;
            var result = new List<Post>();

            if (String.IsNullOrEmpty(title))
            {

                return posts;
            }
            else
            {
                foreach (var post in posts)
                {
                    if (post.Title.ToLower().Trim(' ').Contains(title.Trim(' ').ToLower()))

                    {
                        result.Add(post);
                    }
                }
            }
              
            return result;
        }
            /*
            var word = "programistą";
            var word2 = "zarabia";
   
            var list = new List<string>();
            var result = new List<Post>();

           
            if (String.IsNullOrEmpty(title))
            {

                return _posts;
            }
            else if (String.Equals(title.ToLower().Trim(' '), word2.ToLower()))
            {
                foreach (var post in _posts)
                {
                    list = post.Title.Split(' ').ToList();

                    foreach (var wordInLine in list)
                    {
                        if (wordInLine.ToLower().Contains(word2.ToLower()))
                        {
                            result.Add(post);
                        }
                    }

                   list.Clear();
                }
                return result;
            }
            else if (String.Equals(title.ToLower().Trim(' '), word.ToLower()))
            {
                foreach (var post in _posts)
                {
                    list = post.Title.Split(' ').ToList();

                    foreach (var wordInLine in list)
                    {
                        if (wordInLine.ToLower().Contains(word.ToLower()))
                        {
                            result.Add(post);
                        }
                    }
                    list.Clear();
                }
                return result;
            }
            else
            {
                return result;
            }
            
        }
     */

            public Post Add(Post post)
        {
            post.Id = _posts.Count() + 1;
            post.Created = DateTime.UtcNow;
            _posts.Add(post);
            return post;
        }

        public void Update(Post post)
        {
            post.LastModified = DateTime.UtcNow;
        }

        public void Delete(Post post)
        {
            _posts.Remove(post);
        }

        
        
        
     
    }
}
